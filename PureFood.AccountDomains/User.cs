using PureFood.AccountCommands.Commands;
using PureFood.AccountCommands.Events;
using PureFood.AccountReadModels;
using PureFood.BaseDomains;
using PureFood.Common;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountDomains
{
    [Table("User_tbl")]
    public class User : BaseDomain
    {
        public User(AccountRegisterUserCommand command) : base(command) 
        {
            Id = command.Id;
            Code = command.EmailAddress.AsEmpty();
            EmailAddress = command.EmailAddress.AsEmpty();
            NormalizedEmail = command.NormalizedEmail.AsEmpty().ToLower();
            FullName = command.FullName;
            if (string.IsNullOrEmpty(command.Password))
            {
                command.Password = CommonUtility.GenerateGuid(); 
            }
            Password = EncryptionExtensions.Encryption(Id, command.Password, out var salt);
            PhoneNumber = command.PhoneNumber;
            IsPhoneNumberConfirmed = !string.IsNullOrEmpty(PhoneNumber) && command.IsPhoneNumberConfirmed;
            SecurityStamp = null;
            IsTwoFactorEnabled = command.IsTwoFactorEnabled;
            IsEmailConfirmed = !string.IsNullOrEmpty(EmailAddress) && command.IsEmailConfirmed;
            PasswordSalt = salt;
            AccessFailedCount = 0;
            AvatarUrl = command.AvatarUrl;
            Birthday = command.Birthday;
            Gender = command.Gender;
            Status = AccountStatusEnum.Active;
            Type = AccountTypeEnum.Local;
            OtpType = OtpTypeEnum.OTPByEmail;
            OtpTypeDefault = OtpTypeEnum.OTPByEmail;
            ReferenceCode = null;
        }

        public void Change(AccountChangeCommand command)
        {
            Id = command.Id;
            Code = command.EmailAddress.AsEmpty();
            EmailAddress = command.EmailAddress.AsEmpty();
            NormalizedEmail = command.NormalizedEmail.AsEmpty().ToLower();
            FullName = command.FullName;
            PhoneNumber = command.PhoneNumber;
            IsPhoneNumberConfirmed = !string.IsNullOrEmpty(PhoneNumber) && command.IsPhoneNumberConfirmed;
            SecurityStamp = null;
            IsTwoFactorEnabled = command.IsTwoFactorEnabled;
            IsEmailConfirmed = !string.IsNullOrEmpty(EmailAddress) && command.IsEmailConfirmed;
            AccessFailedCount = 0;
            AvatarUrl = command.AvatarUrl;
            Birthday = command.Birthday;
            Gender = command.Gender;
            Status = command.Status;
            Type = command.Type;
            OtpType = command.OtpType;
            OtpTypeDefault = command.OtpTypeDefault;
            Changed(command);
        }

        public UserAddEvent ToAddEvent()
        {
            return new UserAddEvent()
            {
                ObjectId = Id,
                AccountType = Type
            };
        }

        public bool ComparePassword(string loginPassword)
        {
            string passwordHash = EncryptionExtensions.Encryption(Id, loginPassword, PasswordSalt.AsEmpty());
            return Password?.Equals(passwordHash) == true;
        }

        public void ChangePassword(AccountChangePasswordCommand command)
        {
            var compare = ComparePassword(command.OldPassword!);
            if (compare)
            {
                Password = EncryptionExtensions.Encryption(Id, command.NewPassword!, out string salt);
                PasswordSalt = salt;
                Changed(command);
            }
        }

        public void SetPassword(AccountResetPasswordCommand command)
        {
            Password = EncryptionExtensions.Encryption(Id, command.NewPassword!, out string salt);
            PasswordSalt = salt;
            Changed(command);
        }

        public User(RUser rUser) : base(rUser)
        {
            Id = rUser.Id;
            Code = rUser.Code.AsEmpty();
            EmailAddress = rUser.EmailAddress.AsEmpty();
            NormalizedEmail = rUser.NormalizedEmail.AsEmpty().ToLower();
            FullName = rUser.FullName;
            Password = rUser.Password.AsEmpty();
            PhoneNumber = rUser.PhoneNumber.AsEmpty();
            IsPhoneNumberConfirmed = rUser.IsPhoneNumberConfirmed;
            SecurityStamp = rUser.SecurityStamp;
            IsTwoFactorEnabled = rUser.IsTwoFactorEnabled;
            IsEmailConfirmed = rUser.IsEmailConfirmed;
            PasswordSalt = rUser.PasswordSalt.AsEmpty();
            AccessFailedCount = rUser.AccessFailedCount;
            AvatarUrl = rUser.AvatarUrl.AsEmpty();
            Birthday = rUser.Birthday;
            Gender = rUser.Gender;
            Status = rUser.Status;
            Type = rUser.Type;
            OtpType = rUser.OtpType;
            OtpTypeDefault = rUser.OtpTypeDefault;
            ReferenceCode = rUser.ReferenceCode.AsEmpty();
        }
        public new string Id { get; private set; }
        public string? EmailAddress { get; private set; }
        public string? NormalizedEmail { get; private set; }
        public string? FullName { get; private set; }
        public string? Password { get; private set; }
        public string? PhoneNumber { get; private set; }
        public bool IsPhoneNumberConfirmed { get; private set; }
        public string? SecurityStamp { get; private set; }
        public bool IsTwoFactorEnabled { get; private set; }
        public bool IsEmailConfirmed { get; private set; }
        public string? PasswordSalt { get; private set; }
        public int AccessFailedCount { get; private set; }
        public string? AvatarUrl { get; private set; }
        public DateTime? Birthday { get; private set; }
        public GenderEnum? Gender { get; private set; }
        public AccountStatusEnum Status { get; private set; }
        public AccountTypeEnum Type { get; private set; }
        public OtpTypeEnum? OtpType { get; private set; }
        public OtpTypeEnum OtpTypeDefault { get; set; }
        public string? ReferenceCode { get; private set; }
    }
}
