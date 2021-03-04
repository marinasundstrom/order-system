using Commerce.Application.Deliveries;
using Commerce.Domain.Entities;
using Commerce.Application.Invoices;
using Commerce.Application.Orders;
using Commerce.Application.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Commerce.Infrastructure.Persistence
{
    public static class Generators 
    { 
        public static async Task GenerateData(ApplicationDbContext context)
        {
            OrderDeliveryGenerator orderDeliveryGenerator = new OrderDeliveryGenerator(
             new DeliveryFactory(),
             new SubscriptionDeliveryDatesGenerator());

            foreach (var order in context.Orders.AsQueryable()
                .Include(x => x.Subscription)
                .Include(x => x.Object)
                .Include(x => x.Items)
                .ThenInclude(x => x.Order)
                .Include(x => x.Items)
                .ThenInclude(x => x.Subscription)
                .Include(x => x.Items)
                .ThenInclude(x => x.Object))
            {              
                var deliveries = orderDeliveryGenerator.GetDeliveries(order, null, null);

                foreach (var delivery in deliveries.OrderBy(x => x.PlannedStartDate))
                {
                    context.Deliveries.Add(delivery);

                    delivery.Status = Domain.Enums.DeliveryStatus.Delivered;

                    delivery.ActualStartDate = delivery.PlannedStartDate;
                    delivery.ActualEndDate = delivery.PlannedEndDate;
                }

            }

            await context.SaveChangesAsync();
        }
    }
}
