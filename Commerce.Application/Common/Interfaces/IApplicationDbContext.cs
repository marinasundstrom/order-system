using Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Commerce.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Delivery> Deliveries { get; set; }
        DbSet<DeliveryItem> DeliveryItems { get; set; }
        DbSet<InvoiceItem> InvoiceItems { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<Object> Objects { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Organization> Organizations { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        DbSet<Subscription> Subscriptions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}