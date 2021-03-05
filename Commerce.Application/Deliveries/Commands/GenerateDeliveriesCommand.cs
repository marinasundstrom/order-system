using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Commerce.Application.Common.Interfaces;
using Commerce.Application.Orders;
using Commerce.Application.Subscriptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Application.Deliveries.Commands
{
    public class GenerateDeliveriesCommand : IRequest
    {
        public GenerateDeliveriesCommand(int? orderId)
        {
            OrderId = orderId;
        }

        public int? OrderId { get; }

        public class GenerateDeliveriesCommandHandler : IRequestHandler<GenerateDeliveriesCommand>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GenerateDeliveriesCommandHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(GenerateDeliveriesCommand request, CancellationToken cancellationToken)
            {
                OrderDeliveryGenerator orderDeliveryGenerator = new OrderDeliveryGenerator(
                             new DeliveryFactory(),
                             new SubscriptionDeliveryDatesGenerator());

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
                    var deliveries = orderDeliveryGenerator.GetDeliveries(order, null, null);

                    foreach (var delivery in deliveries.OrderBy(x => x.PlannedStartDate))
                    {
                        applicationDbContext.Deliveries.Add(delivery);

                        delivery.Status = Domain.Enums.DeliveryStatus.Delivered;

                        delivery.ActualStartDate = delivery.PlannedStartDate;
                        delivery.ActualEndDate = delivery.PlannedEndDate;
                    }
                }

                await applicationDbContext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
