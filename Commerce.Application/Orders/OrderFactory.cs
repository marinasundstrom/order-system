using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using Commerce.Domain.ValueObjects;
using System;

namespace Commerce.Application.Orders
{
    public class OrderFactory
    {
        public Order CreateOrder(Order order)
        {
            var targetOrder = new Order();
            UpdateOrder(targetOrder, order);
            return targetOrder;
        }

        public Order CreateOrder(OrderItem orderItem)
        {
            var targerOrder = new Order();
            UpdateOrder(targerOrder, orderItem);
            return targerOrder;
        }

        public OrderItem CreateOrderItem(OrderItem orderItem)
        {
            var targetOrderItem = new OrderItem();
            UpdateOrderItem(targetOrderItem, orderItem);
            return targetOrderItem;
        }

        public void UpdateOrder(Order targetOrder, Order order)
        {
            targetOrder.StatusId = OrderStatuses.Approved.Id;
            targetOrder.StatusDate = DateTime.Now;

            //targetOrder.Order = order;
            targetOrder.Subscription = order.Subscription;
            targetOrder.Object = order.Object;
            targetOrder.Note = order?.Note;
            targetOrder.DeliveryDetails = order?.DeliveryDetails?.Clone();
            //delivery.Assignee = order?.Assignee;
            targetOrder.Created = DateTime.Now;
        }

        public void UpdateOrder(Order targetOrder, OrderItem orderItem)
        {
            targetOrder.StatusId = OrderStatuses.Approved.Id;
            targetOrder.StatusDate = DateTime.Now;

            //targetOrder.Order = orderItem.Order;
            targetOrder.Subscription = orderItem.Subscription ?? orderItem.Order.Subscription;
            targetOrder.Object = orderItem.Object;
            targetOrder.DeliveryDetails = orderItem.HasDeliveryDetails ? orderItem?.DeliveryDetails?.Clone() : orderItem?.Order?.DeliveryDetails?.Clone();
            //delivery.Assignee = orderItem?.Assignee ?? orderItem?.Order?.Assignee;
            targetOrder.Note = orderItem!.Note;

            targetOrder.Created = DateTime.Now;
        }

        public void UpdateOrderItem(OrderItem targetOrderItem, OrderItem orderItem)
        {
            targetOrderItem.Order = orderItem.Order;
            //targetOrderItem.OrderItem = orderItem;
            targetOrderItem.Object = orderItem.Object ?? orderItem?.Order?.Object;
            targetOrderItem.Product = orderItem!.Product;
            targetOrderItem.Quantity = orderItem.Quantity;
            targetOrderItem.Unit = orderItem.Unit;
            targetOrderItem.UnitPrice = orderItem.UnitPrice.Clone();
            targetOrderItem.SubTotal = orderItem.SubTotal.Clone();
            targetOrderItem.Note = orderItem?.Note;
            targetOrderItem.Bill = orderItem?.Bill ?? true;
        }
    }
}
