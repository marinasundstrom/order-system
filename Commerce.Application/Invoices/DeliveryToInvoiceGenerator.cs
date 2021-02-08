using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using System;

namespace Commerce.Application.Invoices
{
    public class DeliveryToInvoiceGenerator : IDeliveryToInvoiceGenerator
    {
        public Invoice GenerateInvoiceFromDelivery(Delivery delivery)
        {
            var invoice = new Invoice()
            {
                InvoiceDate = DateTime.Now.Date,

                Status = InvoiceStatus.Draft,
                StatusDate = DateTime.Now,
                Delivery = delivery,
                Order = delivery.Order,
                OrderItem = delivery?.OrderItem,
                Object = delivery?.Object,
                Subscription = delivery?.Subscription,
                BillingDetails = delivery?.Order?.BillingDetails?.Clone(),

                Note = delivery?.Note,

                Created = DateTime.Now,
                LastModified = DateTime.Now,
            };

            foreach (var deliveryItem in delivery!.Items)
            {
                invoice.Items.Add(new InvoiceItem()
                {
                    Object = deliveryItem.Object,
                    Order = deliveryItem.Order,
                    OrderItem = deliveryItem.OrderItem,
                    Delivery = deliveryItem.Delivery,
                    DeliveryItem = deliveryItem,
                    Product = deliveryItem.Product,
                    Note = deliveryItem.Note,
                    Quantity = deliveryItem.Quantity,
                    SubTotal = deliveryItem.SubTotal.Clone(),
                    Unit = deliveryItem.Unit,
                    UnitPrice = deliveryItem.UnitPrice.Clone()
                });
            }

            return invoice;
        }
    }
}
