using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Commerce.Application.Common.Interfaces;
using Commerce.Application.Subscriptions;
using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Application.Orders.Commands
{
    public class GenerateSubscriptionOrdersCommand : IRequest
    {
        public GenerateSubscriptionOrdersCommand(int? orderId)
        {
            OrderId = orderId;
        }

        public int? OrderId { get; }

        public class GenerateSubscriptionOrdersCommandHandler : IRequestHandler<GenerateSubscriptionOrdersCommand>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GenerateSubscriptionOrdersCommandHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(GenerateSubscriptionOrdersCommand request, CancellationToken cancellationToken)
            {
                SubscriptionOrderGenerator subscriptionOrderGenerator = new SubscriptionOrderGenerator(
                             new OrderFactory(),
                             new SubscriptionOrderDateGenerator());

                var orders = applicationDbContext.Orders.AsQueryable()
                    .Include(o => o.Subscription)
                    .ThenInclude(s => s!.SubscriptionPlan)
                    .Include(o => o.Object)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Order)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .Include(o => o.Items)
                    .ThenInclude(o => o.Subscription)
                    .ThenInclude(s => s!.SubscriptionPlan)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Object)
                    .AsQueryable();

                if(request.OrderId != null)
                {
                    orders = orders.Where(x => x.Id == request.OrderId);
                }

                foreach (var order in orders)
                {
                    var orders2 = subscriptionOrderGenerator.GetOrders(order, null, null).ToArray();

                    foreach (var order2 in orders2.OrderBy(x => x.PlannedStartDate))
                    {
                        applicationDbContext.Orders.Add(order2);

                        order2.OrderDate = DateTime.Now;

                        order2.StatusId = OrderStatuses.Approved.Id;
                        order2.StatusDate = DateTime.Now;

                        // INFO: Just for debug
                        order2.ActualStartDate = order2.PlannedStartDate;
                        order2.ActualEndDate = order2.PlannedEndDate;
                    }
                }

                await applicationDbContext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
