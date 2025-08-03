using PureFood.AccountReadModels;
using PureFood.Common;
using PureFood.ServiceCMS.Shared.Models.User;

namespace PureFood.ServiceCMS.Mappings
{
    public static class UserMapping
    {
        public static UserModel ToModel(RUser rUser)
        {
            return new UserModel
            {
                Code = rUser.Code.AsEmpty(),
                Id = rUser.Id,
                EmailAddress = rUser.EmailAddress,
                NormalizedEmail = rUser.NormalizedEmail,
                FullName = rUser.FullName,
                PhoneNumber = rUser.PhoneNumber,
                IsPhoneNumberConfirmed = rUser.IsPhoneNumberConfirmed,
                IsTwoFactorEnabled = rUser.IsTwoFactorEnabled,
                IsEmailConfirmed = rUser.IsEmailConfirmed,
                AvatarUrl = rUser.AvatarUrl,
                Birthday = rUser.Birthday,
                Gender = rUser.Gender,
                OtpType = rUser.OtpType,
                Type = rUser.Type,
                ReferenceCode = rUser.ReferenceCode,
                OtpTypeDefault = rUser.OtpTypeDefault,
                Status = rUser.Status
            };
        }
    }
}
