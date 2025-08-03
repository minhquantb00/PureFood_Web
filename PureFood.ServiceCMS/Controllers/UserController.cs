using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PureFood.AccountCommands.Queries;
using PureFood.AccountManager.Shared;
using PureFood.AccountReadModels;
using PureFood.BaseApplication.Services;
using PureFood.BaseReadModels;
using PureFood.Common;
using PureFood.Config;
using PureFood.EnumDefine;
using PureFood.ServiceCMS.Mappings;
using PureFood.ServiceCMS.Models.Responses;
using PureFood.ServiceCMS.Shared.Requests.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PureFood.ServiceCMS.Controllers
{
    public class UserController(
        ILogger<CmsBaseController> logger,
        ContextService contextService,
        ICacheService cacheService,
        IAccountService accountService
    )
        : CmsBaseController(logger, contextService)
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<object>> Get([FromBody] AccountGetRequest? request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if(request == null)
                {
                    response.SetFail("Request is null");
                    return;
                }

                var result = await accountService.GetById(new AccountGetByIdQuery
                {
                    ObjectId = request.Id,
                    AccountType = request.AccountType
                });

                if (!result.Status)
                {
                    response.SetFail(result.Message);
                    return;
                }

                response.Data = UserMapping.ToModel(result.Data!);

                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> GetByIds([FromBody] AccountGetByIdsRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    response.SetFail("Request is null");
                    return;
                }

                var result = await accountService.GetByIds(new AccountGetByIdsQuery
                {
                    Ids = request.Ids,
                });

                if (!result.Status)
                {
                    response.SetFail(result.Message);
                    return;
                }

                response.Data = result.Data?.Select(p => UserMapping.ToModel(p)).ToArray() ?? Array.Empty<object>();
                response.TotalRow = result.Data?.Length;
                response.SetSuccess();
            });
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<object>> RegisterUser([FromBody] AccountRegisterUserRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    response.SetFail("Request is null");
                    return;
                }
                var result = await accountService.RegisterUser(new AccountCommands.Commands.AccountRegisterUserCommand
                {
                    EmailAddress = request.EmailAddress,
                    FullName = request.FullName,
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber,
                    IsPhoneNumberConfirmed = request.IsPhoneNumberConfirmed,
                    AvatarUrl = request.AvatarUrl,
                    Birthday = request.Birthday,
                    Gender = request.Gender,
                    IsEmailConfirmed = request.IsEmailConfirmed,
                    IsTwoFactorEnabled = request.IsTwoFactorEnabled,
                    NormalizedEmail =  request.NormalizedEmail,
                });
                if (!result.Status)
                {
                    response.SetFail(result.Message);
                    return;
                }
                response.SetSuccess();
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<object>> Login([FromBody] AccountLoginRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }


                var tokenResult = await accountService.Login(new AccountCommands.Commands.AccountLoginCommand()
                {
                    Email = request.Email!,
                    Password = request.Password!,
                    RememberMe = request.RememberMe == true,
                    ClientId = ContextService.ClientId(),
                    LoginProvider = ExternalLoginProviderEnum.LocalId,
                    IP = ContextService.GetIp(),
                    LoginUid = string.Empty,
                    ObjectId = request.Email,
                    ProviderKey = string.Empty,
                    ProcessUid = string.Empty,
                    DeviceLoginInfo = request.DeviceInfo,
                    ExternalLoginConfigId = request.ExternalLoginConfigId,
                });
                if (!tokenResult.Status)
                {
                    response.SetFail(tokenResult.Messages);
                    return;
                }

                if (tokenResult.Data == null || string.IsNullOrEmpty(tokenResult.Data.User.Id))
                {
                    response.SetFail("Login invalid");
                    return;
                }

                if (tokenResult.Data.User is { TwoFactorEnabled: true, OtpVerify: false })
                {
                    response.Data = new UserLoginResponse()
                    {
                        Id = tokenResult.Data.User.Id,
                        OtpType = tokenResult.Data.User.OtpType,
                        SessionId = tokenResult.Data.SessionId,
                        IsNeedOtpVerify = true
                    };
                    response.SetSuccess();
                    return;
                }

                response.Data = await LoginProcess(tokenResult.Data);
                response.SetSuccess();
            });
        }
        [HttpPost]
        public async Task<BaseResponse<object>> ChangePassword([FromBody] AccountChangePasswordRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var user = ContextService.UserInfoRequired();

                var result = await accountService.ChangePassword(new AccountCommands.Commands.AccountChangePasswordCommand
                {
                    ObjectId = user.Id,
                    OldPassword = request.OldPassword,
                    NewPassword = request.NewPassword,
                    ConfirmNewPassword = request.ConfirmNewPassword,
                });
                if (!result.Status)
                {
                    response.SetFail(result.Message);
                    return;
                }
                response.SetSuccess();
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<object>> ResetPassword([FromBody] AccountResetPasswordRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var result = await accountService.ResetPassword(new AccountCommands.Commands.AccountResetPasswordCommand
                {
                    ConfirmCode = request.VerificationCode,
                    NewPassword = request.NewPassword,
                    ConfirmNewPassword = request.ConfirmNewPassword,
                });
                if (!result.Status)
                {
                    response.SetFail(result.Message);
                    return;
                }
                response.SetSuccess();
            });
        }
        [HttpPost]
        public async Task<BaseResponse<object>> Change([FromBody] AccountChangeRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if(request == null)
                {
                    LogError("Request is null");
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }

                var user = ContextService.UserInfoRequired();

                var result = await accountService.Change(new AccountCommands.Commands.AccountChangeCommand
                {
                    AvatarUrl = request.AvatarUrl,
                    EmailAddress = request.EmailAddress,
                    Birthday = request.Birthday,
                    FullName = request.FullName,
                    Gender = request.Gender,
                    Id = user.Id,
                    IsEmailConfirmed = request.IsEmailConfirmed,
                    IsPhoneNumberConfirmed = request.IsPhoneNumberConfirmed,
                    IsTwoFactorEnabled = request.IsTwoFactorEnabled,
                    LoginUid = user.LoginUid,
                    NormalizedEmail = request.NormalizedEmail,
                    ObjectId = user.Id,
                    OtpType = request.OtpType,
                    OtpTypeDefault = request.OtpTypeDefault,
                    PhoneNumber = request.PhoneNumber,
                    ProcessUid = user.Id,
                    Status = request.Status,
                    Type = request.Type
                });

                if(!result.Status)
                {
                    LogError(result.Message);
                    response.SetFail(result.Message);
                    return;
                }

                response.SetSuccess();
            });
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<object>> ForgotPassword([FromBody] AccountForgotPasswordRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var result = await accountService.ForgotPassword(new AccountCommands.Commands.AccountForgotPasswordCommand
                {
                    EmailOrMobile = request.EmailOrMobile!,
                    Type = request.Type,
                });
                if (!result.Status)
                {
                    response.SetFail(result.Message);
                    return;
                }
                response.SetSuccess();
            });
        }


        #region Private Methods
        private async Task SignIn(RLoginModel loginModel)
        {
            string uniqueNameKey = JwtRegisteredClaimNames.UniqueName;
            string userName = loginModel.User.DisplayName.AsEmpty();
            var newClaims = new List<Claim>
        {
            new(ContextService.SessionCode, loginModel.SessionId),
            new("MinuteExpire", loginModel.MinuteExpire.ToString()),
            new(uniqueNameKey, userName),
            new(ClaimTypes.Name, userName),
            new(JwtRegisteredClaimNames.Sid, loginModel.User.Id)
        }; 
            var claimsIdentity = new ClaimsIdentity(newClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(loginModel.MinuteExpire),
                IsPersistent = true,
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
            int refreshTokenExpiresTime = ConfigSettingEnum.RefreshTokenExpiresTime
                .GetConfig().AsInt();
            if (refreshTokenExpiresTime <= 0)
            {
                refreshTokenExpiresTime = TimeSpan.FromDays(30).TotalMinutes.AsInt();
            }

            HttpContext.Response.Cookies.Append(Constant.CookieRefreshToken,
                loginModel.User.RefToken, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddMinutes(refreshTokenExpiresTime)
                });
        }

        private async Task<UserLoginCompletedResponse> LoginProcess(RLoginModel loginModel, string? returnUrl = "")
        {
            await SignIn(loginModel);
            AuthenCodeModel authenCodeModel = new AuthenCodeModel()
            {
                SessionId = loginModel.SessionId,
                AuthenApplicationId = loginModel.User.AuthenApplicationId.AsEmpty(),
                ExternalLoginConfigId = loginModel.User.ExternalLoginConfigId.AsEmpty(),
                AuthenCodeHashKey = Constant.AuthenCodeHashKey,
                Token = loginModel.Token
            };
            var authenCode = Serialize.JsonSerializeObject(authenCodeModel);
            string authenCodeEncrypt = EncryptionExtensions.GenerateCodeChallenge(authenCode);
            await cacheService.Set(authenCodeModel, authenCodeEncrypt, TimeSpan.FromMinutes(10));
            var result = new UserLoginCompletedResponse
            {
                Id = loginModel.User.Id,
                OtpType = loginModel.User.OtpType,
                SessionId = loginModel.SessionId,
                IsNeedOtpVerify = false,
                Token = loginModel.Token,
                MinuteExpire = loginModel.MinuteExpire,
                RefreshToken = loginModel.User.RefToken,
                ReturnUrl = returnUrl,
                AuthenCode = authenCodeEncrypt,
                AuthenApplicationId = loginModel.User.AuthenApplicationId.AsEmpty(),
                CurrentDealerId = loginModel.User.CurrentDealerId.AsEmpty(),
            };
            return result;
        }
        #endregion
    }
}
