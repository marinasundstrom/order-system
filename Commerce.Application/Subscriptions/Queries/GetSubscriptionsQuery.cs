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
    public class GetSubscriptionsQuery : IRequest<IEnumerable<Subscription>>
    {
        public class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsQuery, IEnumerable<Subscription>>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetSubscriptionsQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<IEnumerable<Subscription>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.Subscriptions
                    .Include(x => x.SubscriptionPlan)
                    .Include(x => x.Order)
                    .Include(x => x.OrderItem)
                    .Include(x => x.Deliveries)               
                    .OrderBy(x => x.StartDate)
                    .AsSplitQuery()
                    .AsNoTracking()
                    .ToArrayAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
