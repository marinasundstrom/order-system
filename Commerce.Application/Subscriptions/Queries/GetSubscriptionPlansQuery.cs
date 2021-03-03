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
    public class GetSubscriptionPlansQuery : IRequest<IEnumerable<SubscriptionPlan>>
    {
        public class GetSubscriptionPlansQueryHandler : IRequestHandler<GetSubscriptionPlansQuery, IEnumerable<SubscriptionPlan>>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetSubscriptionPlansQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<IEnumerable<SubscriptionPlan>> Handle(GetSubscriptionPlansQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.SubscriptionPlans              
                    .AsSplitQuery()
                    .ToArrayAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
