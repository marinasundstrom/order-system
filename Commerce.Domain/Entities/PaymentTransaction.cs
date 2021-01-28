using Commerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Entities
{
    class PaymentTransaction
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int OrderId { get; set; }

        public int DeliveryId { get; set; }

        public int? InvoiceId { get; set; }

        public PaymentTransactionType TransactionType { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string? PayerRef { get; set; }

        public string? PayeeRef { get; set; }

        public string? TransactionRef { get; set; }

        public CurrencyAmount Amount { get; set; } = null!;
    }

    public enum PaymentTransactionType
    {
        Payment,
        Return,
        Cashback
    }

    public enum PaymentMethod
    {
        Visa,
        MasterCard,
        PostGiro,
        BankGiro,
        Bitcoin
    }
}
