using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Application.Invoices
{
    public class DeliveryInvoiceGenerator
    {
        private readonly IApplicationDbContext applicationDbContext;

        public DeliveryInvoiceGenerator(IApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Invoice>> GenerateInvoicesAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = applicationDbContext.Deliveries
                .Include(d => d.Order)
                    .ThenInclude(d => d.Subscription)
                .Include(d => d.Order)
                    .ThenInclude(d => d.Object)
                .Include(d => d.Order)
                    .ThenInclude(d => d.Items)
                .Include(d => d.Order)
                    .ThenInclude(d => d.Deliveries)
                .Include(d => d.Object)
                .Include(d => d.Subscription)
                    .ThenInclude(d => d!.SubscriptionPlan)
                .Include(d => d.Items)
                    .ThenInclude(d => d.Product)
                .Include(d => d.Items)
                    .ThenInclude(d => d.Object)
                .Include(d => d.Items)
                    .ThenInclude(d => d.Delivery)
                .Where(d => d.Status == DeliveryStatus.Delivered)
                .AsQueryable();

            if(fromDate != null)
            {
                query = query.Where(d => d.ActualStartDate >= fromDate);
            }

            if (toDate != null)
            {
                query = query.Where(d => d.ActualEndDate <= toDate);
            }

            var results = await query.ToArrayAsync();

            var orderDeliveryGroups = results
                .GroupBy(x => x.Order);

            List<Invoice> invoices = new List<Invoice>();

            foreach(var orderDeliveries in orderDeliveryGroups)
            {
                var order = orderDeliveries.Key;

                var invoice = new Invoice()
                {
                    InvoiceDate = DateTime.Now.Date,

                    Status = InvoiceStatus.Draft,
                    StatusDate = DateTime.Now,
                    Order = order,
                    Object = order?.Object,
                    Subscription = order?.Subscription,
                    BillingDetails = order?.BillingDetails?.Clone(),

                    Note = order?.Note,

                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                };

                foreach(Delivery delivery in orderDeliveries)
                {
                    foreach (DeliveryItem deliveryItem in delivery.Items)
                    {
                        if (!deliveryItem.Bill)
                            continue;

                        invoice.Items.Add(new InvoiceItem()
                        {
                            Object = deliveryItem.Object ?? delivery?.Object,
                            Order = deliveryItem.Order ?? delivery!.Order,
                            OrderItem = deliveryItem?.OrderItem ?? delivery?.OrderItem,
                            Delivery = deliveryItem?.Delivery ?? delivery,
                            DeliveryItem = deliveryItem,
                            Product = deliveryItem!.Product,
                            Subscription = delivery?.Subscription,
                            Note = deliveryItem.Note,
                            Quantity = deliveryItem.Quantity,
                            SubTotal = deliveryItem.SubTotal.Clone(),
                            Unit = deliveryItem.Unit,
                            UnitPrice = deliveryItem.UnitPrice.Clone()
                        });
                    }
                }

                if (invoice.Items.Any())
                {
                    // Calculate totals

                    invoices.Add(invoice);
                }
            }

            return invoices;
        }
    }
}
