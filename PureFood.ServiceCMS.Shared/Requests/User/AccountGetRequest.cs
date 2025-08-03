using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ServiceCMS.Shared.Requests.User
{
    public record AccountGetsRequest
    {
        public string? KeyWord { get; set; }
        public AccountTypeEnum? AccountType { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public AccountStatusEnum? Status { get; set; }
        public string[]? DepartmentIdsExclude { get; set; }
    }

    public record AccountGetRequest
    {
        public string? Id { get; set; }
        public AccountTypeEnum AccountType { get; set; }
    }

    public record AccountGetByIdsRequest
    {
        public string[]? Ids { get; set; }
    }

    public record AccountRegisterUserRequest
    {
        public string? EmailAddress { get; set; }
        public string? NormalizedEmail { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
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
    }

    public record AccountLoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Recaptcha { get; set; }
        public bool? RememberMe { get; set; }
        public string? DeviceInfo { get; set; }
        public string? ExternalLoginConfigId { get; set; }
    }

    public record AccountChangePasswordRequest
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }
    }

    public record AccountResetPasswordRequest
    {
        public string? VerificationCode { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }
    }

    public record AccountChangeRequest
    {
        public string? Id { get; set; }
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
    }

    public record AccountForgotPasswordRequest
    {
        public string EmailOrMobile { get; set; }
        public OtpTypeEnum? Type { get; set; }
    }
}
