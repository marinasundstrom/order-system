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
    public class GetDeliveriesQuery : IRequest<IEnumerable<Delivery>>
    {
        public GetDeliveriesQuery(int? orderId)
        {
            OrderId = orderId;
        }

        public int? OrderId { get; }

        public class GetDeliveriesQueryHandler : IRequestHandler<GetDeliveriesQuery, IEnumerable<Delivery>>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetDeliveriesQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<IEnumerable<Delivery>> Handle(GetDeliveriesQuery request, CancellationToken cancellationToken)
            {
                var deliveries = applicationDbContext.Deliveries
                    .Include(x => x.InvoiceItem)
                    .Include(x => x.Order)
                    .Include(x => x.Subscription)
                    .OrderByDescending(x => x.ActualStartDate)
                    .ThenByDescending(x => x.PlannedStartDate)
                    .AsSplitQuery();

                if(request.OrderId != null)
                {
                    deliveries = deliveries.Where(d => d.Order.Id == request.OrderId);
                }

                return await deliveries.ToArrayAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
