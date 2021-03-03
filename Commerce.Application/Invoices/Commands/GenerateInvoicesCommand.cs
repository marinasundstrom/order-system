using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Commerce.Application.Billing;
using Commerce.Application.Common.Interfaces;
using MediatR;

namespace Commerce.Application.Invoices.Commands
{
    public class GenerateInvoicesCommand : IRequest
    {
        public class GenerateInvoicesCommandHandler : IRequestHandler<GenerateInvoicesCommand>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GenerateInvoicesCommandHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(GenerateInvoicesCommand request, CancellationToken cancellationToken)
            {
                await DeleteInvoices();

                DeliveryInvoiceGenerator deliveryInvoiceGenerator = new DeliveryInvoiceGenerator(applicationDbContext);

                var invoices = await deliveryInvoiceGenerator.GenerateInvoicesAsync();

                applicationDbContext.Invoices.AddRange(invoices);

                await applicationDbContext.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task DeleteInvoices()
            {
                foreach (var invoice in applicationDbContext.Invoices.ToList())
                {
                    applicationDbContext.Invoices.Remove(invoice);
                }

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
