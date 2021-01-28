using System;
using System.Collections.Generic;

namespace Commerce.Domain.Entities
{
    public class Delivery
    {
        public int Id { get; set; }

        public Order Order { get; set; } = null!;

        public OrderItem? OrderItem { get; set; }

        public Object? Object { get; set; }

        public Invoice? Invoice { get; set; }

        public Subscription? Subscription { get; set; }

        public DateTime PlannedStartDate { get; set; }

        public DateTime? PlannedEndDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// A Window of time in which to deliver
        /// </summary>
        public DeliveryFlexibility? Flexibility { get; set; }

        public DeliveryDetails? DeliveryDetails { get; set; }

        //public Person? Assignee { get; set; }

        //public DateTime? LastAssigned { get; set; }

        public string? Note { get; set; }

        public List<DeliveryItem> Items { get; } = new List<DeliveryItem>();

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public Delivery SetDeliveryDate(DateTime start, DateTime? end)
        {
            this.PlannedStartDate = start;
            this.PlannedEndDate = end;

            return this;
        }
    }
}
