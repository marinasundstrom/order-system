using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using Commerce.Infrastructure.Identity;
using Commerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

#nullable disable

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<DeliveryItem> DeliveryItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Domain.Entities.Object> Objects { get; set; }

        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

#nullable restore

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
