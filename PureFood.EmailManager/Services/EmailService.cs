using MimeKit;
using PureFood.EmailCommands.Commands;
using PureFood.EmailManager.Shared;
using PureFood.EmailReadModels;
using MailKit.Net.Smtp;

namespace PureFood.EmailManager.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailService(EmailConfiguration emailConfig) => _emailConfig = emailConfig;
        public async Task<string> SendEmail(SendMessageCommand message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
            var recipients = string.Join(", ", message.To);
            return DataResponseMessage.GetEmailSuccessMessage(recipients);
        }

        private MimeMessage CreateEmailMessage(SendMessageCommand message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            emailMessage.To.AddRange(message.To.Select(email => MailboxAddress.Parse(email)));
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
