using Commerce.Domain.Enums;
using Commerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Commerce.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ProductType ProductType { get; set; }

        public ProductCategory Category { get; set; }

        public string? Description { get; set; }

        public TimeSpan? EstimatedPreparationTime { get; set; }

        public CurrencyAmount Price { get; set; } = null!;

        public CurrencyAmount? CompareAtPrice { get; set; }

        public ICollection<SubscriptionPlan> SubscriptionPlans { get; } = new HashSet<SubscriptionPlan>();

        public List<OrderItem> OrderItems { get; } = new List<OrderItem>();

        public List<DeliveryItem> DeliveryItems { get; } = new List<DeliveryItem>();
    }
}
