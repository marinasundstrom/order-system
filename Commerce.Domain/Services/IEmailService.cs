using System.Threading.Tasks;

namespace Commerce.Domain.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string receiver, string subject, string htmlMessage);
    }
}
