 using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PopcornBackend.MailHelper;
using System.Security.Authentication;

namespace PopcornBackend.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings emailSettings;
        private readonly IConfiguration config;

        public EmailService(IOptions<EmailSettings> emailSettings, IConfiguration config)
        {
            this.emailSettings = emailSettings.Value;
            this.config = config;
        }

        public async Task SendEmailAsync(Mailrequest mailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(emailSettings.Email);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            Console.WriteLine(emailSettings.Host + " " + emailSettings.Password);

            smtp.Connect(emailSettings.Host, emailSettings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
            smtp.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
            smtp.Authenticate(emailSettings.Email, emailSettings.Password); 
            await smtp.SendAsync(email);

            smtp.Disconnect(true);
            
        }
    }
}
