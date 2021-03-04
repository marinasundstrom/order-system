using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using Commerce.Infrastructure.Identity;
using Commerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

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

        public DbSet<BillingPlan> BillingPlans { get; set; }

#nullable restore

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLoggerFactory(
                LoggerFactory.Create(builder => { builder.AddConsole(); }));

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Order!)
                .WithOne(oi => oi.Subscription!)
                .HasForeignKey<Subscription>(s => s.OrderId);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.OrderItem!)
                .WithOne(oi => oi.Subscription!)
                .HasForeignKey<Subscription>(s => s.OrderItemId);

            modelBuilder.Entity<Delivery>()
                .HasOne(s => s.InvoiceItem!)
                .WithOne(oi => oi.Delivery!)
                .HasForeignKey<InvoiceItem>(s => s.DeliveryId);
        }
    }
}
