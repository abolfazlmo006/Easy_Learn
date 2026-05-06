using System.Net;
using System.Net.Mail;

namespace Easy_learn.WebApi.Services
{
    public class MessageSender : IMessageSender
    {
        private readonly IConfiguration _configuration;

        public MessageSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false)
        {
            using (var client = new SmtpClient())
            {
                var credentials = new NetworkCredential()
                {
                    UserName = _configuration["EmailSetting:Email"], // without @gmail.com
                    Password = _configuration["EmailSetting:Password"]
                };
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                client.Host = _configuration["EmailSetting:Host"];
                client.Port = int.Parse(_configuration["EmailSetting:Port"]);
                client.EnableSsl = true;
                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(toEmail) },
                    From = new MailAddress(_configuration["EmailSetting:Email_Complete"]), // with @gmail.com
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = isMessageHtml
                };

                client.Send(emailMessage);
            }

        }
    }
}
