using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using PureFood.AccountCommands.Commands;
using PureFood.AccountCommands.Events;
using PureFood.AccountCommands.Queries;
using PureFood.AccountDomains;
using PureFood.AccountManager.Shared;
using PureFood.AccountManager.Validations;
using PureFood.AccountReadModels;
using PureFood.AccountRepository;
using PureFood.BaseApplication.Services;
using PureFood.BaseCommands;
using PureFood.BaseReadModels;
using PureFood.Common;
using PureFood.Config;
using PureFood.EmailCommands.Commands;
using PureFood.EmailManager.Shared;
using PureFood.EnumDefine;
using PureFood.ESRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PureFood.AccountManager.Services
{
    public class AccountService(
        ILogger<AccountService> logger,
        ContextService contextService,
        IESRepository esRepository,
        AuthenService authenService,
        IForgotPasswordRepository forgotPasswordRepository,
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IEmailService emailService
    ) : BaseService(logger, contextService), IAccountService
    {
        private readonly HashSet<string> _adminUserIds = ConfigSettingEnum.AdminUserIds.GetConfig()
        .Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToHashSet();


        public async Task<BaseCommandResponse<RUser>> GetById(AccountGetByIdQuery query)
        {
            return await ProcessCommand<RUser>(async (response) =>
            {

                if (query == null)
                {
                    LogError("Query is null");
                    return;
                }

                var result = await userRepository.Get(query.ObjectId!);
                if (result == null)
                {
                    LogError($"User with ID {query.ObjectId} not found.");
                    response.SetFail("user is null");
                    return;
                }
                response.Data = result;

                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RUser[]>> GetByIds(AccountGetByIdsQuery query)
        {
            return await ProcessCommand<RUser[]>(async (response) =>
            {
                if (query == null || query.Ids == null || query.Ids.Length == 0)
                {
                    LogError("Query or Ids are null or empty");
                    response.SetFail("ids is null or empty");
                    return;
                }
                var results = await userRepository.Gets(query.Ids);
                if (results == null || results.Length == 0)
                {
                    LogError($"No users found for IDs: {string.Join(", ", query.Ids)}");
                    response.SetFail("users not found");
                    return;
                }
                response.Data = results;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RUser>> GetByPhoneNumber(AccountGetByPhoneNumberQuery query)
        {
            return await ProcessCommand<RUser>(async (response) =>
            {
                if (query == null || string.IsNullOrEmpty(query.PhoneNumber))
                {
                    LogError("Query or PhoneNumber is null or empty");
                    response.SetFail("phone number is null or empty");
                    return;
                }
                var result = await userRepository.GetByUserNameOrEmailOrPhoneNumber(query.PhoneNumber);
                if (result == null)
                {
                    LogError($"User with PhoneNumber {query.PhoneNumber} not found.");
                    response.SetFail("user is null");
                    return;
                }
                response.Data = result;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RUser[]?>> Gets(AccountGetsQuery query)
        {
            return await ProcessCommand<RUser[]?>(async (response) =>
            {
                if (query == null)
                {
                    LogError("Query is null");
                    response.SetFail("query is null");
                    return;
                }
                var paging = new RefSqlPaging(query.PageIndex, query.PageSize);
                var results = await userRepository.Search(query, paging);
                if (results == null || results.Length == 0)
                {
                    LogError("No users found for the given query.");
                    response.SetFail("users not found");
                    return;
                }

                response.Data = results;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RLoginModel>> Login(AccountLoginCommand command)
        {
            return await ProcessCommand<RLoginModel>(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("command is null");
                    return;
                }

                var validate = AccountLoginValidator.ValidateModel(command);
                if (!validate.IsValid)
                {
                    LogError("Validation failed: " + string.Join(", ", validate.Errors.Select(e => e.ErrorMessage)));
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
                    return;
                }

                RUser? account;

                if (command.LoginProvider is EnumDefine.ExternalLoginProviderEnum.Google or
                   EnumDefine.ExternalLoginProviderEnum.Apple or
                   EnumDefine.ExternalLoginProviderEnum.Microsoft or
                   EnumDefine.ExternalLoginProviderEnum.Zalo)
                {
                    if (string.IsNullOrEmpty(command.ObjectId))
                    {
                        LogError("ObjectId is required for external login providers.");
                        response.SetFail("ObjectId is required for external login providers.");
                        return;
                    }
                    account = await userRepository.Get(command.ObjectId!);
                }
                else
                {
                    if (string.IsNullOrEmpty(command.Email))
                    {
                        LogError("Email and Password is invalid");
                        response.SetFail("Email and Password is invalid");
                        return;
                    }
                    account = await userRepository.GetByUserNameOrEmailOrPhoneNumber(command.Email!);
                }

                if (account == null)
                {
                    LogError("Email or password is invalid");
                    response.SetFail("Email or password is invalid");
                    return;
                }

                if (account.Status != EnumDefine.AccountStatusEnum.Active)
                {
                    LogError("Account is not active");
                    response.SetFail("Account is not active");
                    return;
                }

                var user = new User(account);
                if (command.LoginProvider is not EnumDefine.ExternalLoginProviderEnum.Google and
                   not EnumDefine.ExternalLoginProviderEnum.Apple and
                   not EnumDefine.ExternalLoginProviderEnum.Microsoft and
                   not EnumDefine.ExternalLoginProviderEnum.Zalo)
                {
                    var verifyPassword = user.ComparePassword(command.Password!);

                    if (!verifyPassword)
                    {
                        LogError("Email or password is invalid");
                        response.SetFail("Email or password is invalid");
                        return;
                    }
                }

                bool otpVerify = user.IsTwoFactorEnabled != true;
                var userLogin =
                await BuildAccountLoginInfo(user,
                    otpVerify,
                    0,
                    command.ClientId,
                    command.RememberMe,
                    command.ExternalLoginConfigId,
                    command.ExternalLoginId,
                    command.ExternalToken,
                    command.ExternalRefreshToken,
                    command.LoginProvider
                );

                var tokenInfo = await CreateToken(
                    command.Email.AsEmpty(),
                    command.RememberMe == true,
                    userLogin,
                    string.Empty
                );


                RUserDeviceMapping? rUserDeviceMapping = null;

                if (tokenInfo.Token.Length > 0)
                {
                    UserDeviceMappingAddCommand userDeviceMappingAddCommand = new UserDeviceMappingAddCommand()
                    {
                        Token = string.Empty,
                        FcmToken = string.Empty,
                        ClientId = command.ClientId,
                        ParentId = string.Empty,
                        RememberMe = command.RememberMe,
                        DeviceLoginInfo = command.DeviceLoginInfo,
                        IP = command.IP,
                        ExpireDate = userLogin.ExpireDate,
                        LoginUid = userLogin.LoginUid,
                        ObjectId = command.ObjectId,
                        ProcessDate = command.ProcessDate,
                        ProcessUid = userLogin.Id,
                        SessionId = userLogin.SessionId
                    };
                    var userDeviceMapping =
                        new UserDeviceMapping(userDeviceMappingAddCommand, userLogin.RefToken);


                }
                response.Data = new RLoginModel
                {
                    User = userLogin,
                    MinuteExpire = tokenInfo.Item2,
                    Token = tokenInfo.Item1,
                    SessionId = userLogin.SessionId,
                    UserDeviceMapping = rUserDeviceMapping
                };
                response.SetSuccess();
                EventAdd(new UserLoginEvent()
                {
                    IP = command.IP.AsEmpty(),
                    Date = command.ProcessDate,
                    DeviceInfo = command.DeviceLoginInfo,
                    ObjectId = userLogin.Id,
                    ProcessUid = userLogin.Id,
                    LoginUid = userLogin.LoginUid,
                });
            });
        }

        public async Task<BaseCommandResponse> RegisterUser(AccountRegisterUserCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                var validate = AccountRegisterUserValidator.ValidateModel(command);
                if (!validate.IsValid)
                {
                    LogError("Validation failed: " + string.Join(", ", validate.Errors.Select(e => e.ErrorMessage)));
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
                    return;
                }

                command.Id = "PureFood" + DateTime.Now.Ticks.ToString();
                var user = new User(command);
                await userRepository.Add(user);
                var permission = new UserRole(user.Id, "User", EnumDefine.StatusEnum.New);
                await userRoleRepository.Add(permission);
                EventAdd(user.ToAddEvent());
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> ChangePassword(AccountChangePasswordCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }

                if (!command.NewPassword!.Equals(command.ConfirmNewPassword))
                {
                    LogError("Password do not match");
                    response.SetFail("Password do not match");
                    return;
                }

                var ruser = await userRepository.Get(command.ObjectId!);
                if (ruser == null)
                {
                    LogError($"User with ID {command.ObjectId} not found.");
                    response.SetFail("User not found");
                    return;
                }
                User user = new User(ruser!);

                user.ChangePassword(command);

                await userRepository.Change(user);

                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> ForgotPassword(AccountForgotPasswordCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }

                if(!CommonUtility.IsEmail(command.EmailOrMobile!) && !CommonUtility.IsValidVietnamesePhoneNumber(command.EmailOrMobile!))
                {
                    LogError("Invalid email and phone number format");
                    response.SetFail("Invalid email and phone number format");
                    return;
                }

                var user = await userRepository.GetByUserNameOrEmailOrPhoneNumber(command.EmailOrMobile!);

                if(user == null)
                {
                    LogError($"User with Email or PhoneNumber {command.EmailOrMobile} not found.");
                    response.SetFail("User not found");
                    return;
                }

                ForgotPassword forgotPassword = new ForgotPassword(command, user, new AuthenticatorSecretKey(OtpTypeEnum.OTPByEmail, "", "", true, StatusEnum.New));
                forgotPassword.Id = CommonUtility.GenerateGuid();

                await forgotPasswordRepository.Add(forgotPassword);

                var message = new SendMessageCommand(new string[] { user.EmailAddress }, "Nhận mã xác nhận tại đây: ", $"Mã xác nhận là: {forgotPassword.VerificationCode}");
                await emailService.SendEmail(message);

                response.SetSuccess();

            });
        }

        public async Task<BaseCommandResponse> ResetPassword(AccountResetPasswordCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if(command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }

                var forgotPassword = await forgotPasswordRepository.GetByCode(command.ConfirmCode!);
                if(forgotPassword == null)
                {
                    LogError("Invalid confirm code");
                    response.SetFail("Invalid confirm code");
                    return;
                }
                if (forgotPassword.IsExpired)
                {
                    LogError("Confirm code is expired");
                    response.SetFail("Confirm code is expired");
                    return;
                }

                if (!command.NewPassword!.Equals(command.ConfirmNewPassword!))
                {
                    LogError("Password do not match");
                    response.SetFail("Password do not match");
                    return;
                }

                var user = await userRepository.Get(forgotPassword.UserId!);
                if (user == null)
                {
                    LogError($"User with ID {forgotPassword.UserId} not found.");
                    response.SetFail("User not found");
                    return;
                }

                User userDomain = new User(user!);
                userDomain.SetPassword(command);

                await userRepository.Change(userDomain);

                response.SetSuccess();

            });
        }

        public async Task<BaseCommandResponse> Change(AccountChangeCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if (command is null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }

                var user = await userRepository.Get(command.Id!);
                if(user is null)
                {
                    LogError($"User with ID {command.Id} not found.");
                    response.SetFail("User not found");
                    return;
                }

                var userDomain = new User(user!);
                userDomain.Change(command);

                await userRepository.Change(userDomain);

                response.SetSuccess();
            });
        }

        #region Private Methods
        private async Task<(string Token, int MinuteExpire)> CreateToken(
        string userName,
        bool remember,
        AccountLoginInfo accountLoginInfo,
        string oldRefreshToken)
        {
            if (!string.IsNullOrEmpty(oldRefreshToken))
            {
                await authenService.RemoveLoginInfo(oldRefreshToken);
            }

            var sessionKey = accountLoginInfo.SessionId;
            var minuteExpire = remember
                ? ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt() + 60
                : ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt();
            if (accountLoginInfo.OtpVerify)
            {
                accountLoginInfo.MinuteExpire = minuteExpire;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(ConfigSettingEnum.JwtTokensKey.GetConfig());
                string uniqueNameKey = JwtRegisteredClaimNames.UniqueName;
                List<Claim> claims =
                [
                    new(ContextService.SessionCode, sessionKey),
                new("MinuteExpire", minuteExpire.ToString()),
                new(uniqueNameKey, userName),
                new(JwtRegisteredClaimNames.Sid, accountLoginInfo.Id)
                ];
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = Extension.GetCurrentDate().AddMinutes(minuteExpire),
                    SigningCredentials =
                        new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenValue = tokenHandler.WriteToken(token);
                accountLoginInfo.Token = tokenValue;
                await authenService.SetLoginInfo(sessionKey, accountLoginInfo, accountLoginInfo.MinuteExpire);
                return (tokenValue, minuteExpire);
            }

            return (string.Empty, 5);
        }

        private async Task<AccountLoginInfo> BuildAccountLoginInfo(
        User user,
        bool otpVerify,
        int minuteExpire,
        string clientId,
        bool rememberMe,
        string? externalLoginConfigId = null,
        string? externalLoginId = null,
        string? externalToken = null,
        string? externalRefreshToken = null,
        ExternalLoginProviderEnum externalLoginProvider = 0,
        string? authenApplicationId = null)
        {
            //        var currentUser = _contextService.UserInfo();
            var userLogin = new AccountLoginInfo()
            { 
                PhoneNumber = user.PhoneNumber,
                Email = user.EmailAddress,
                FullName = user.FullName,
                Id = user.Id,
                Code = user.Code,
                RefToken = CommonUtility.GenerateGuid(), // string.IsNullOrEmpty(refToken) ? : refToken,
                OtpVerify = otpVerify,
                Version = user.Version,
                AvatarUrl = user.AvatarUrl,
                OtpType = user.OtpType ?? OtpTypeEnum.OTPByEmail,
                InitDate = DateTime.Now,
                MinuteExpire = minuteExpire,
                LoginUid = user.Id,
                CurrentPartnerId = string.Empty, //(currentUser?.CurrentPartnerId).AsEmpty(),
                CurrentDealerId = string.Empty, //(currentUser?.CurrentDealerId).AsEmpty(),
                ClientId = clientId,
                OTPSendCount = 0,
                RememberMe = rememberMe,
                CurrentLanguageId = string.Empty, //(currentUser?.CurrentLanguageId).AsEmpty(),
                TwoFactorEnabled = user.IsTwoFactorEnabled,
                SessionId = $"SESSIONID{CommonUtility.GenerateGuid()}",
                AccountType = user.Type,
                IsAdministrator = _adminUserIds.Contains(user.Id),
                ExternalLoginConfigId = externalLoginConfigId,
                ExternalLoginId = externalLoginId,
                ExternalLoginToken = externalToken,
                ExternalLoginProvider = externalLoginProvider,
                ExternalRefreshToken = externalRefreshToken,
                AuthenApplicationId = authenApplicationId
            };

            if (string.IsNullOrEmpty(userLogin.CurrentDealerId))
            {
                return userLogin;
            }

            userLogin.PermissionReloadVersion += 1;
            return userLogin;
        }

        
        #endregion
    }
}
