using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanFolderWatchdog.Common.Services
{
    internal class MailNotificationService : INotificationService
    {
        public void Send(string toAddress, string toName, string subject, string message, string attachment)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Jeffry Steegmans", "jeffrysteegmans@gmail.com"));
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
                client.Connect("smtp.gmail.com", 587);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("jeffrysteegmans@gmail.com", "yaeemwwzgknceuth");

                client.Send(mailMessage);
                client.Disconnect(true);
            }
        }
    }
}
