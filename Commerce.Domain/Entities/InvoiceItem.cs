using Commerce.Domain.Enums;
using Commerce.Domain.ValueObjects;

namespace Commerce.Domain.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        public Invoice Invoice { get; set; } = null!;

        public Order Order { get; set; } = null!;

        public int OrderId { get; set; }

        public OrderItem? OrderItem { get; set; }

        public int? OrderItemId { get; set; }

        public Delivery? Delivery { get; set; }

        public int? DeliveryId { get; set; }

        public DeliveryItem? DeliveryItem { get; set; }

        public int? DeliveryItemId { get; set; }

        public Product Product { get; set; } = null!;

        public Object? Object { get; set; }

        public Subscription? Subscription { get; set; }

        public CurrencyAmount UnitPrice { get; set; } = null!;

        public double Quantity { get; set; }

        public OrderUnit? Unit { get; set; }

        public CurrencyAmount SubTotal { get; set; } = null!;

        public string? Note { get; set; }
    }
}