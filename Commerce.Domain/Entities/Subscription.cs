using Commerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commerce.Domain.Entities
{
    public class Subscription
    {
        public int Id { get; set; }

        public Person? Person { get; set; }

        public Organization? Organization { get; set; }

        public Order? Order { get; set; }

        public int? OrderId { get; set; }

        public OrderItem? OrderItem { get; set; }

        public int? OrderItemId { get; set; }

        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public SubscriptionStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public bool? AutoRenew { get; set; }
        
        public string? Note { get; set; }

        public List<Delivery> Deliveries { get; } = new List<Delivery>();

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime DeletedDate { get; set; }

    }
}
