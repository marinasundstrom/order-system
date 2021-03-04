using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using Commerce.Domain.ValueObjects;
using System;

namespace Commerce.Application.Deliveries
{
    public class DeliveryFactory
    {
        public Delivery CreateDelivery(Order order)
        {
            var delivery = new Delivery();
            UpdateDelivery(delivery, order);
            return delivery;
        }

        public Delivery CreateDelivery(OrderItem orderItem)
        {
            var delivery = new Delivery();
            UpdateDelivery(delivery, orderItem);
            return delivery;
        }

        public DeliveryItem CreateDeliveryItem(OrderItem orderItem)
        {
            var deliveryItem = new DeliveryItem();
            UpdateDeliveryItem(deliveryItem, orderItem);
            return deliveryItem;
        }

        public void UpdateDelivery(Delivery delivery, Order order)
        {
            delivery.Status = DeliveryStatus.Scheduled;
            delivery.StatusDate = DateTime.Now;

            delivery.Order = order;
            delivery.Subscription = order.Subscription;
            delivery.Object = order.Object;
            delivery.Note = order?.Note;
            delivery.DeliveryDetails = order?.DeliveryDetails?.Clone();
            //delivery.Assignee = order?.Assignee;
            delivery.Created = DateTime.Now;
        }

        public void UpdateDelivery(Delivery delivery, OrderItem orderItem)
        {
            delivery.Status = DeliveryStatus.Scheduled;
            delivery.StatusDate = DateTime.Now;

            delivery.Order = orderItem.Order;
            delivery.OrderItem = orderItem;
            delivery.Subscription = orderItem.Subscription ?? orderItem.Order.Subscription;
            delivery.Object = orderItem.Object;
            delivery.DeliveryDetails = orderItem.HasDeliveryDetails ? orderItem?.DeliveryDetails?.Clone() : orderItem?.Order?.DeliveryDetails?.Clone();
            //delivery.Assignee = orderItem?.Assignee ?? orderItem?.Order?.Assignee;
            delivery.Note = orderItem!.Note;

            delivery.Created = DateTime.Now;
        }

        public void UpdateDeliveryItem(DeliveryItem deliveryItem, OrderItem orderItem)
        {
            deliveryItem.Order = orderItem.Order;
            deliveryItem.OrderItem = orderItem;
            deliveryItem.Object = orderItem.Object ?? orderItem?.Order?.Object;
            deliveryItem.Product = orderItem!.Product;
            deliveryItem.Quantity = orderItem.Quantity;
            deliveryItem.Unit = orderItem.Unit;
            deliveryItem.UnitPrice = orderItem.UnitPrice.Clone();
            deliveryItem.SubTotal = orderItem.SubTotal.Clone();
            deliveryItem.Note = orderItem?.Note;
            deliveryItem.Bill = orderItem?.Bill ?? true;
        }
    }
}
