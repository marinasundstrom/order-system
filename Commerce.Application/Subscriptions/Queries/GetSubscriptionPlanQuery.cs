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
    public class GetSubscriptionPlanQuery : IRequest<SubscriptionPlan>
    {
        public GetSubscriptionPlanQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class GetSubscriptionPlanQueryHandler : IRequestHandler<GetSubscriptionPlanQuery, SubscriptionPlan>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetSubscriptionPlanQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<SubscriptionPlan> Handle(GetSubscriptionPlanQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.SubscriptionPlans
                    .AsSplitQuery()
                    .FirstAsync(x => x.Id == request.Id);
            }
        }
    }
}
