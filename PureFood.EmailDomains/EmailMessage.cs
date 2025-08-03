using PureFood.BaseDomains;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailDomains
{
    [Table("EmailMessage_tbl")]
    public class EmailMessage : BaseDomain
    {
        public EmailMessage()
        {
            this.BodyFormat = MailBodyFormat.Html;
            this.Priority = MailPriority.Normal;

            this.To = new List<EmailAddress>();
            this.Cc = new List<EmailAddress>();
            this.Bcc = new List<EmailAddress>();
            this.ReplyTo = new List<EmailAddress>();

            this.Attachments = new List<Attachment>();

            this.Headers = new NameValueCollection();
        }

        public EmailMessage(string to, string subject, string body, string from)
            : this()
        {
            this.To.Add(new EmailAddress(to));
            this.Subject = subject;
            this.Body = body;
            this.From = new EmailAddress(from);
        }

        public EmailMessage(EmailAddress to, string subject, string body, EmailAddress from)
            : this()
        {
            this.To.Add(to);
            this.Subject = subject;
            this.Body = body;
            this.From = from;
        }
        public new string Id { get; private set; }
        public EmailAddress From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AltText { get; set; }

        public MailBodyFormat BodyFormat { get; set; }
        public MailPriority Priority { get; set; }

        public ICollection<EmailAddress> To { get; private set; }
        public ICollection<EmailAddress> Cc { get; private set; }
        public ICollection<EmailAddress> Bcc { get; private set; }
        public ICollection<EmailAddress> ReplyTo { get; private set; }

        public IEnumerable<Attachment> Attachments { get; private set; }

        public NameValueCollection Headers { get; private set; }

        public async void BodyFromFile(string filePathOrUrl)
        {
            StreamReader sr;

            if (filePathOrUrl.ToLower().StartsWith("http"))
            {
                var wc = new WebClient();
                sr = new StreamReader(await wc.OpenReadTaskAsync(filePathOrUrl));
            }
            else
            {
                sr = new StreamReader(filePathOrUrl, Encoding.Default);
            }

            this.Body = await sr.ReadToEndAsync();

            sr.Close();
        }
    }
}
