using PureFood.AccountReadModels;
using PureFood.BaseCommands;
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
    [Table("AuthenticatorSecretKey_tbl")]
    public class AuthenticatorSecretKey : BaseDomain
    {
        public AuthenticatorSecretKey(RAuthenticatorSecretKey authenticatorSecretKey) : base(authenticatorSecretKey)
        {
            Id = authenticatorSecretKey.Id;
            Type = authenticatorSecretKey.Type;
            Key = authenticatorSecretKey.Key;
            Info = authenticatorSecretKey.Info;
            IsDefault = authenticatorSecretKey.IsDefault;
            Status = authenticatorSecretKey.Status;
        }

        public AuthenticatorSecretKey(
        OtpTypeEnum type,
        string key,
        string info,
        bool isDefault,
        StatusEnum status)
        {
            Id = CommonUtility.GenerateGuid();
            Type = type;
            Key = key;
            Info = info;
            IsDefault = isDefault;
            Status = status;
        }
        public new string Id { get; set; }
        public OtpTypeEnum Type { get; set; }
        public string Key { get; set; }
        public string Info { get; set; }
        public bool IsDefault { get; set; }
        public StatusEnum Status { get; set; }
    }
}
