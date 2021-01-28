using System.Threading.Tasks;

namespace Commerce.Application.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string receiver, string subject, string htmlMessage);
    }
}
