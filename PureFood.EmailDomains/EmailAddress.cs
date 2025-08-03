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
    [Table("EmailAddress_tbl")]
    public class EmailAddress : BaseDomain
    {
        private readonly MailAddress _inner;

        public EmailAddress(string address)
        {
            _inner = new MailAddress(address);
        }

        public EmailAddress(string address, string displayName)
        {
            _inner = new MailAddress(address, displayName);
        }

        public EmailAddress(MailAddress address)
        {
            _inner = address;
        }

        public string Address
        {
            get { return _inner.Address; }
        }

        public string DisplayName
        {
            get { return _inner.DisplayName; }
        }

        public string User
        {
            get { return _inner.User; }
        }

        public string Host
        {
            get { return _inner.Host; }
        }

        public override int GetHashCode() => _inner.GetHashCode();

        public override string ToString() => _inner.ToString();

        public MailAddress ToMailAddress() => _inner;

        public static implicit operator string(EmailAddress obj)
        {
            return obj.ToString();
        }
    }
}
