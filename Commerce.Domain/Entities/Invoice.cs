using Commerce.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Commerce.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public InvoiceStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public Order? Order { get; set; } = null!;

        public Object? Object { get; set; }

        public Subscription? Subscription { get; set; }

        public BillingDetails? BillingDetails { get; set; }

        public string? Note { get; set; }

        public List<InvoiceItem> Items { get; } = new List<InvoiceItem>();

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
