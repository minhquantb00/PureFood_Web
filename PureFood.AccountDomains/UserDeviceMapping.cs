using PureFood.AccountCommands.Commands;
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
    [Table("UserDeviceMapping_tbl")]
    public class UserDeviceMapping : BaseDomain
    {
        public UserDeviceMapping(UserDeviceMappingAddCommand command, string id) : base(command)
        {
            Id = Code = id;
            Token = command.Token.AsEmpty();
            DeviceLoginInfo = command.DeviceLoginInfo;
            IP = command.IP.AsEmpty();
            ParentId = command.ParentId.AsEmpty();
            ExpireDate = command.ExpireDate;
            ClientId = command.ClientId.AsEmpty();
            Status = StatusEnum.Active;
            LoginType = command.LoginType;
            RememberMe = true;
            FCMToken = command.FcmToken.AsEmpty();
            SessionId = command.SessionId;
        }
        public new string Id { get; private set; }
        public string? Token { get; private set; }
        public string? DeviceLoginInfo { get; private set; }
        public string? IP { get; private set; }
        public string? ParentId { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public string ClientId { get; private set; }
        public bool RememberMe { get; private set; }
        public StatusEnum Status { get; private set; }
        public LoginTypeEnum LoginType { get; private set; }
        public string? FCMToken { get; private set; }
        public string UserDeviceId => $"{CreatedUid}_{ClientId}";
        public string? SessionId { get; private set; }
    }
}
