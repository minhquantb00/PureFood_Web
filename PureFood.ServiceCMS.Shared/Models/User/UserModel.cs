using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ServiceCMS.Shared.Models.User
{

    public record UserModel
    {
        public required string Id { get; set; }
        public required string Code { get; set; }
        public string? EmailAddress { get; set; }
        public string? NormalizedEmail { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? Birthday { get; set; }
        public GenderEnum? Gender { get; set; }
        public AccountStatusEnum Status { get; set; }
        public AccountTypeEnum Type { get; set; }
        public OtpTypeEnum? OtpType { get; set; }
        public OtpTypeEnum OtpTypeDefault { get; set; }

        public string? ReferenceCode { get; set; }
        public ExternalLoginModel[]? ExternalLogins { get; set; }
        public AccountSettingModel[]? AccountSettings { get; set; }
    }


    public record ExternalLoginModel
    {
        public required string Id { get; set; }
        public ExternalLoginProviderEnum LoginProvider { get; set; }
        public string? LoginProviderName { get; set; }
        public string? ProviderKey { get; set; }
        public required string UserId { get; set; }
        public string? Info { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Locale { get; set; }
        public StatusEnum Status { get; set; }
        public GenderEnum? Gender { get; set; }
        public int? BirthYear { get; set; }
        public int? BirthMonth { get; set; }
        public int? BirthDay { get; set; }
    }

    public record AccountSettingModel
    {
        public required string Id { get; set; }
        public required string UserId { get; set; }
        public required AccountSettingEnum Type { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
        public StatusEnum Status { get; set; }
    }



    public record UserChangeModel : UserModel
    {
    }
}
