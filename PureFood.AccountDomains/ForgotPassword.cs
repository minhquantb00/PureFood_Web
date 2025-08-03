using PureFood.AccountCommands.Commands;
using PureFood.AccountReadModels;
using PureFood.BaseDomains;
using PureFood.Common;
using PureFood.Config;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountDomains
{
    [Table("ForgotPassword_tbl")]
    public class ForgotPassword : BaseDomain
    {
        public ForgotPassword(AccountForgotPasswordCommand command, RUser user, AuthenticatorSecretKey authenticatorSecretKey) :
        base(command)
        {
            VerificationCode = "Pure_" + new Random().Next(1000, 9999).ToString();
            Code = "Pure_" + new Random().Next(1000, 9999).ToString();
            UserId = user.Id;
            EmailOrMobile = command.EmailOrMobile;
            Status = ForgotPasswordStatusEnum.New;
            ExpiresTime = Constant.ForgotPasswordExpiresTime;
            Expired = CreatedDate.AddMinutes(ExpiresTime);
            AuthenticatorSecretKeyObj = authenticatorSecretKey;
        }
        public ForgotPassword(RForgotPassword rForgotPassword) : base(rForgotPassword)
        {
            Id = rForgotPassword.Id;
            UserId = rForgotPassword.UserId;
            EmailOrMobile = rForgotPassword.EmailOrMobile;
            Expired = rForgotPassword.Expired;
            ExpiresTime = rForgotPassword.ExpiresTime;
            AuthenticatorSecretKeyObj = rForgotPassword.AuthenticatorSecretKeyObj == null
                ? null
                : new AuthenticatorSecretKey(rForgotPassword.AuthenticatorSecretKeyObj);
            Status = rForgotPassword.Status;
            VerificationCode = rForgotPassword.VerificationCode;
        }
        public new string Id { get; set; }
        public string UserId { get; set; }
        public string EmailOrMobile { get; set; }
        public ForgotPasswordStatusEnum Status { get; set; }
        public DateTime Expired { get; set; }
        public int ExpiresTime { get; set; }
        public AuthenticatorSecretKey? AuthenticatorSecretKeyObj { get; set; }
        public string? AuthenticatorSecretKey => Common.Serialize.JsonSerializeObject(AuthenticatorSecretKeyObj);
        public bool IsExpired => Expired < DateTime.Now;
        public string? VerificationCode { get; set; }
    }
}
