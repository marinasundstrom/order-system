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
    public class GetInvoicesQuery : IRequest<IEnumerable<Invoice>>
    {
        public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, IEnumerable<Invoice>>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetInvoicesQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<IEnumerable<Invoice>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.Invoices
                    .Include(x => x.Order)
                    .Include(x => x.Subscription)
                    .OrderByDescending(x => x.InvoiceDate)
                    .AsSplitQuery()
                    .ToArrayAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
