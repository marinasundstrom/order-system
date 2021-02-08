using System;
using System.Collections.Generic;
using Commerce.Domain.Entities;

namespace Commerce.Application.Orders
{
    public interface IOrderDeliveryGenerator
    {
        IEnumerable<Delivery> GenerateDeliveries(Order order, DateTime? startDate = null, DateTime? endDate = null);
    }
}