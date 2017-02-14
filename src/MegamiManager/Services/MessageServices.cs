using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegamiManager.Services
{
    public class SendGridMessageSender : IEmailSender
    {
        private string sendGridKey;
        private string sendGridFrom;

        public SendGridMessageSender(string sendGridKey, string sendGridFrom)
        {
            this.sendGridKey = sendGridKey;
            this.sendGridFrom = sendGridFrom;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(sendGridKey);
            var from = new EmailAddress(sendGridFrom);
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message.Replace("\n", "\n<br>"));
            var response = await client.SendEmailAsync(msg);
        }
    }

    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : ISmsSender
    {
        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
