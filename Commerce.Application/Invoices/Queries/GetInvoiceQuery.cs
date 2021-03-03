using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Commerce.Application.Invoices.Queries
{
    public class GetInvoiceQuery : IRequest<Invoice>
    {
        public GetInvoiceQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, Invoice>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetInvoiceQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<Invoice> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.Invoices
                    .Include(x => x.Order)
                    .Include(x => x.Subscription)
                    .Include(x => x.Items)
                    .ThenInclude(x => x.OrderItem)
                    .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                    .Include(x => x.Items)
                    .ThenInclude(x => x.Delivery)
                    .Include(x => x.Items)
                    .ThenInclude(x => x.DeliveryItem)
                    .AsSplitQuery()
                    .FirstAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            }
        }
    }
}
