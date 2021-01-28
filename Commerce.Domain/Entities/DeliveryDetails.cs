using Commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace Commerce.Domain.Entities
{
    [Owned]
    public class DeliveryDetails : ICloneable
    {
        public Name Name { get; set; } = null!;

        public Address Address { get; set; } = null!;

        object ICloneable.Clone()
        {
            return Clone();
        }

        public DeliveryDetails Clone()
        {
            return JsonSerializer.Deserialize<DeliveryDetails>(
                JsonSerializer.Serialize(this))!;
        }
    }
}
