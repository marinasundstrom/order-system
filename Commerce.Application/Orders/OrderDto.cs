using Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Application.Orders
{
    public class OrderDto
    {
        public OrderDto(Order order)
        {
            Id = order.Id;
            OrderDate = order.OrderDate;
            Status = order.Status;
            StatusDate = order.StatusDate;
            DeliveryDetails = order.DeliveryDetails != null ? new DeliveryDetailsDto(order.DeliveryDetails) : null;
            //BillingDetails = order.BillingDetails != null ? new BillingDetailsDto(order.BillingDetails) : null;
            Items = order.Items.ConvertAll(orderItem => new OrderItemDto(orderItem));
        }

        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public Domain.Enums.OrderStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        //public Person? Person { get; set; }

        //public Organization? Organization { get; set; }

        //public Object? Object { get; set; }

        //public string? CustomerReference { get; set; }

        //public string? OurReference { get; set; }

        //public string? Note { get; set; }

        public DeliveryDetailsDto? DeliveryDetails { get; set; } // Non-null=? = new DeliveryDetails();

        public BillingDetailsDto? BillingDetails { get; set; } // Non-null

        //public Subscription? Subscription { get; set; }

        //public int? SubscriptionId { get; set; }

        ////public Person? Assignee { get; set; }

        ////public DateTime? LastAssigned { get; set; }


        public List<OrderItemDto> Items { get; } = new List<OrderItemDto>();

        //public List<Delivery> Deliveries { get; } = new List<Delivery>();
    }
}
