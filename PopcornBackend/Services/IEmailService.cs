using PopcornBackend.MailHelper;

namespace PopcornBackend.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Mailrequest mailRequest);
    }
}
