using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Commerce.Application.Deliveries.Queries
{
    public class GetDeliveryQuery : IRequest<Delivery>
    {
        public GetDeliveryQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class GetDeliveryQueryHandler : IRequestHandler<GetDeliveryQuery, Delivery>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetDeliveryQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<Delivery> Handle(GetDeliveryQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.Deliveries
                    .Include(x => x.InvoiceItem)
                    .Include(x => x.Order)
                    .Include(x => x.OrderItem)
                    .Include(x => x.Items)
                    .ThenInclude(x => x.OrderItem)
                    .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                    .AsSplitQuery()
                    .FirstAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            }
        }
    }
}
