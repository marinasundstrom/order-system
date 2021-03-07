using Commerce.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Commerce.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public Order? ParentOrder { get; set; }

        public int? ParentOrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; } = null!;

        public int StatusId { get; set; }

        public DateTime StatusDate { get; set; }

        public DateTime PlannedStartDate { get; set; }

        public DateTime? PlannedEndDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        public Person? Person { get; set; }

        public Organization? Organization { get; set; }

        public Object? Object { get; set; }

        public string? CustomerReference { get; set; }

        public string? OurReference { get; set; }

        public string? Note { get; set; }

        public InvoiceItem? InvoiceItem { get; set; }

        public DeliveryDetails? DeliveryDetails { get; set; } // Non-null=? = new DeliveryDetails();

        public BillingDetails? BillingDetails { get; set; } // Non-null

        public Subscription? Subscription { get; set; }

        public int? SubscriptionId { get; set; }

        //public Person? Assignee { get; set; }

        //public DateTime? LastAssigned { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public List<OrderItem> Items { get; } = new List<OrderItem>();

        public List<Order> SubOrders { get; } = new List<Order>();

        public List<Delivery> Deliveries { get; } = new List<Delivery>();

        public Order SetPlannedDates(DateTime start, DateTime? end)
        {
            this.PlannedStartDate = start;
            this.PlannedEndDate = end;

            return this;
        }
    }
}
