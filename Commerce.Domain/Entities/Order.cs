using Commerce.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Commerce.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public Person? Person { get; set; }

        public Organization? Organization { get; set; }

        public Object? Object { get; set; }

        public string? CustomerReference { get; set; }

        public string? OurReference { get; set; }

        public string? Note { get; set; }

        public DeliveryDetails? DeliveryDetails { get; set; } // Non-null=? = new DeliveryDetails();

        public BillingDetails? BillingDetails { get; set; } // Non-null

        public Subscription? Subscription { get; set; }

        //public Person? Assignee { get; set; }

        //public DateTime? LastAssigned { get; set; }


        public List<OrderItem> Items { get; } = new List<OrderItem>();

        public List<Delivery> Deliveries { get; } = new List<Delivery>();
    }
}
