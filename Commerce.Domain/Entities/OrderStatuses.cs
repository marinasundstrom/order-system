using Commerce.Domain.Entities;

namespace Commerce.Domain.Entities
{
    public class OrderStatuses
    {
        public static OrderStatus Created { get; } = new OrderStatus("Created");

        public static OrderStatus Saved { get; } = new OrderStatus("Saved");

        public static OrderStatus Approved { get; } = new OrderStatus("Approved");

        public static OrderStatus Voided { get; } = new OrderStatus("Voided");

        public static OrderStatus Completed { get; } = new OrderStatus("Completed");

        public static OrderStatus PayerActionRequired { get; } = new OrderStatus("PayerActionRequired");
    }
}