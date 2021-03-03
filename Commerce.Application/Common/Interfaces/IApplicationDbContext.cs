using Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Commerce.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Delivery> Deliveries { get; }
        DbSet<DeliveryItem> DeliveryItems { get; }
        DbSet<InvoiceItem> InvoiceItems { get; }
        DbSet<Invoice> Invoices { get; }
        DbSet<Object> Objects { get; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Order> Orders { get; }
        DbSet<Organization> Organizations { get; }
        DbSet<Person> Persons { get; }
        DbSet<Product> Products { get; }
        DbSet<SubscriptionPlan> SubscriptionPlans { get; }
        DbSet<Subscription> Subscriptions { get; }
        DbSet<BillingPlan> BillingPlans { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}