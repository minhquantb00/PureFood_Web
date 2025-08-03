using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailDomains
{
    [Table("EmailAccount_tbl")]
    public class EmailAccount : BaseDomain
    {
        public new string Id { get; private set; }
        public string Email { get; private set; }
        public string DisplayName { get; private set; }
        public string Host { get; private set; }
        public int Port { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool EnableSsl { get; private set; }
        public bool UseDefaultCredentials { get; private set; }
        public string FriendlyName
        {
            get
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return Email;

                return $"{DisplayName} ({Email})";
            }
        }
        public string Singnature { get; set; }
        public bool IsDefault { get; set; }

        public EmailAccount Clone()
        {
            return (EmailAccount)this.MemberwiseClone();
        }

        public EmailAddress ToEmailAddress()
        {
            return new EmailAddress(this.Email, this.DisplayName);
        }
    }
}
