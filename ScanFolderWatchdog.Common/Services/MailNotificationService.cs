using MailKit.Net.Smtp;
using MimeKit;

namespace ScanFolderWatchdog.Common.Services
{
    internal class MailNotificationService : INotificationService
    {
        private readonly MailNotificationSettings _settings;

        public MailNotificationService(INotificationSettings settings)
        {
            _settings = (MailNotificationSettings)settings;
        }

        public void Send(string toAddress, string toName, string subject, string message, string attachment)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(toName, toAddress));
            mailMessage.To.Add(new MailboxAddress(toName, toAddress));
            mailMessage.Subject = subject;

            var builder = new BodyBuilder
            {
                TextBody = message
            };
            builder.Attachments.Add(attachment);

            mailMessage.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_settings.Host, _settings.Port);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(_settings.Username, _settings.Password);

                client.Send(mailMessage);
                client.Disconnect(true);
            }
        }
    }
}
