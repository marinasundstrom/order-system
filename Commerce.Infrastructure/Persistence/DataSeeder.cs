using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using Commerce.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Commerce.Infrastructure.Persistence.TestData;

namespace Commerce.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(this IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                await context.Database.EnsureDeletedAsync();

                await context.Database.EnsureCreatedAsync();

                context.Orders.AddRange(new[] {
                    CreateOrderWith2Items(),
                    CreateOrderWith2ItemsAndOneWithInlineAddress(),
                    CreateOrderWith2SubscriptionItems(),
                    CreateSubscriptionOrderWith1Item(),
                    CreateSubscriptionOrderWith2Items(),
                    CreateOrderWithInlineDeliveryAddressesAndInlineSubscription()
                 });

                context.OrderStatus.AddRange(typeof(OrderStatuses).GetProperties().Select(x => (OrderStatus)x.GetValue(null)!));

                await context.SaveChangesAsync();
            }
        }
    }
}
