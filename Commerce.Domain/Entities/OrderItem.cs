using Commerce.Domain.Enums;
using Commerce.Domain.ValueObjects;
using System.Collections.Generic;

namespace Commerce.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public Order Order { get; set; } = null!;

        public Product Product { get; set; } = null!;

        public Object? Object { get; set; }

        public Subscription? Subscription { get; set; }

        public int? SubscriptionId { get; set; }

        public CurrencyAmount UnitPrice { get; set; } = null!;

        public double Quantity { get; set; }

        public OrderUnit? Unit { get; set; }

        public CurrencyAmount SubTotal { get; set; } = null!;

        //public Person? Assignee { get; set; }

        //public DateTime? LastAssigned { get; set; }

        public string? Note { get; set; }

        public bool Invoice { get; set; } = true;

        public bool HasDeliveryDetails { get; set; } = false;

        public DeliveryDetails? DeliveryDetails { get; set; }

        public List<Delivery> Deliveries { get; } = new List<Delivery>();
    }
}
