using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Commerce.Application.Orders.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<Order>>
    {
        public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Order>>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetOrdersQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<IEnumerable<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.Orders
                    .Include(x => x.Status)
                    .Include(x => x.Subscription)
                    .OrderByDescending(x => x.OrderDate)
                    //.OrderByDescending(x => x.ActualStartDate)
                    //.ThenByDescending(x => x.PlannedStartDate)
                    .AsSplitQuery()
                    .ToArrayAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
