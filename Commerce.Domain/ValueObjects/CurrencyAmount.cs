using Commerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace Commerce.Domain.ValueObjects
{
    [Owned]
    public class CurrencyAmount : ICloneable
    {
        public decimal Value { get; set; }

        public Currency Currency { get; set; }

        public static CurrencyAmount operator *(CurrencyAmount currencyAmount, decimal factor)
        {
            return new CurrencyAmount
            {
                Currency = currencyAmount.Currency,
                Value = currencyAmount.Value * factor
            };
        }

        public CurrencyAmount Clone()
        {
            return new CurrencyAmount
            {
                Currency = Currency,
                Value = Value
            };
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public override string ToString()
        {
            return $"{Value} {Currency.ToString()}";
        }
    }
}
