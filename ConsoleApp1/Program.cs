using Commerce.Application.Deliveries;
using Commerce.Domain.Entities;
using Commerce.Infrastructure.Persistence;
using Commerce.Application.Invoices;
using Commerce.Application.Orders;
using Commerce.Application.Subscriptions;
using Commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using static Commerce.Infrastructure.Persistence.TestData;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //var ssn = (Ssn)"900105-3835";

            //var ssn = (Ssn)"19900132-3835";

            //var subscriptionDeliveryDatesGenerator = new SubscriptionDeliveryDatesGenerator();

            //foreach (var subscription in subscriptions)
            //{
            //    var dates = subscriptionDeliveryDatesGenerator.GetDeliveryDatesFromSubscription(subscription);

            //    foreach (var date in dates)
            //    {
            //        Console.WriteLine(date);
            //    }

            //    Console.WriteLine();
            //}

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("Data Source=test.db;");


            ApplicationDbContext context = new ApplicationDbContext(dbContextOptionsBuilder.Options);

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            OrderDeliveryGenerator orderDeliveryGenerator = new OrderDeliveryGenerator(
                new DeliveryFactory(),
                new SubscriptionDeliveryDatesGenerator());

            context.Orders.AddRange(new[] {
                CreateOrderWith2Items(),
                CreateOrderWith2ItemsAndOneWithInlineAddress(),
                CreateOrderWith2SubscriptionItems(),
                CreateSubscriptionOrderWith1Item(),
                CreateSubscriptionOrderWith2Items(),
                CreateOrderWithInlineDeliveryAddressesAndInlineSubscription()
             });

            await context.SaveChangesAsync();

            await Generators.GenerateData(context);
        }
    }
}
