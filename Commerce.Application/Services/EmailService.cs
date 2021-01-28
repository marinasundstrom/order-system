using System;
using System.Threading.Tasks;

namespace Commerce.Application.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string receiver, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
