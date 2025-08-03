using PureFood.BaseDomains;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountDomains
{
    [Table("DeviceCryptography_tbl")]
    public class DeviceCryptography : BaseDomain
    {
        public new string Id { get; set; }
        public string? ServerRSAPrivateKey { get; set; }
        public string? ServerRSAPublicKey { get; set; }
        public string? DeviceRSAPrivateKey { get; set; }
        public string? DeviceRSAPublicKey { get; set; }
        public string? AuthenticatorSecretKey { get; set; }
        public byte[]? AESIV { get; set; }
        public DeviceCryptographyTypeEnum Type { get; set; }
        public StatusEnum Status { get; set; }
    }
}
