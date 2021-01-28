using Commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace Commerce.Domain.Entities
{
    [Owned]
    public class BillingDetails : ICloneable
    {
        public Name Name { get; set; } = null!;

        public Address Address { get; set; } = null!;

        //public PaymentMethod PaymentMethod { get; set; }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public BillingDetails Clone()
        {
            return JsonSerializer.Deserialize<BillingDetails>(
                JsonSerializer.Serialize(this))!;
        }
    }
}
