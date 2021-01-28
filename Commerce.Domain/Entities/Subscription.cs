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

        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public SubscriptionStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public bool? AutoRenew { get; set; }
        
        public string? Note { get; set; }

        public List<Delivery> Deliveries { get; } = new List<Delivery>();

        [InverseProperty(nameof(Entities.Order.Subscription))]
        public Order Order { get; set; } = null!;

        [InverseProperty(nameof(Entities.OrderItem.Subscription))]
        public OrderItem? OrderItem { get; set; }
    }
}
