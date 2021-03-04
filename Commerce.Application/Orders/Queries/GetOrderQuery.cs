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
    public class GetOrderQuery : IRequest<Order>
    {
        public GetOrderQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetOrderQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.Orders
                    .Include(x => x.Subscription)
                    .ThenInclude(x => x!.SubscriptionPlan)
                    .Include(x => x.Items)
                    .ThenInclude(x => x.Product)               
                    .Include(x => x.Items)
                    .Include(x => x.Items)
                    .ThenInclude(x => x.Subscription)
                    .ThenInclude(x => x!.SubscriptionPlan)
                    .Include(x => x.Deliveries)
                    .ThenInclude(x => x.Order)
                    .AsSplitQuery()
                    .FirstAsync(x => x.Id == request.Id);
            }
        }
    }
}
