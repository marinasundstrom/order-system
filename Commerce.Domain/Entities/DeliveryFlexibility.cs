using Microsoft.EntityFrameworkCore;
using System;

namespace Commerce.Domain.Entities
{
    [Owned]
    public class DeliveryFlexibility
    {
        public DateTime StartFrom { get; set; }

        public DateTime StartTo { get; set; }

        public DateTime? EndFrom { get; set; }

        public DateTime? EndTo { get; set; }
    }
}