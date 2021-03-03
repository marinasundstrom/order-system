using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Commerce.Application.Subscriptions.Queries
{
    public class GetSubscriptionQuery : IRequest<Subscription>
    {
        public GetSubscriptionQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, Subscription>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetSubscriptionQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<Subscription> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.Subscriptions
                    .Include(x => x.SubscriptionPlan)
                    .Include(x => x.Order)
                    .Include(x => x.OrderItem)
                    .Include(x => x.Deliveries)
                    .ThenInclude(x => x.Order)
                    .Include(x => x.Deliveries)
                    .ThenInclude(x => x.OrderItem)
                    .Include(x => x.Deliveries)
                    .ThenInclude(x => x.Items)
                    .Include(x => x.Deliveries)
                    .ThenInclude(x => x.Items)
                    .ThenInclude(x => x.Product)
                    .AsSplitQuery()
                    .AsNoTracking()
                    .FirstAsync(x => x.Id == request.Id);
            }
        }
    }
}
