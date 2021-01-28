using System.Collections.Generic;

namespace Commerce.Domain.Entities
{
    public class Object
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public Person? Person { get; set; }

        public Organization? Organization { get; set; }

        public List<Order> Orders { get; } = new List<Order>();

        public List<OrderItem> OrderItems { get; } = new List<OrderItem>();

        public List<Delivery> Deliveries { get; } = new List<Delivery>();

        public List<DeliveryItem> DeliveryItems { get; } = new List<DeliveryItem>();
    }
}
