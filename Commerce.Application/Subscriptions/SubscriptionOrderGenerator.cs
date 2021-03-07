using Commerce.Application.Deliveries;
using Commerce.Domain.Entities;
using Commerce.Application.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Commerce.Application.Orders;

namespace Commerce.Application.Subscriptions
{
    public class SubscriptionOrderGenerator
    {
        private OrderFactory orderFactory;
        private readonly SubscriptionOrderDateGenerator orderDateGenerator;

        public SubscriptionOrderGenerator(
            OrderFactory orderFactory,
            SubscriptionOrderDateGenerator orderDateGenerator)
        {
            this.orderFactory = orderFactory;
            this.orderDateGenerator = orderDateGenerator;
        }

        public IEnumerable<Order> GetOrders(Order order, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (order.Subscription == null)
            {
                // Order (Non-Subscription Order)

                var subOrder = orderFactory.CreateOrder(order);
                //.SetDeliveryDate(date.Start, date.End);

                foreach (var orderItem in order.Items)
                {
                    /* if (orderItem.Subscription == null && orderItem.HasDeliveryDetails)
                    {
                        // Deliver to separate address. Its own Delivery.

                        var subOrder2 = orderFactory.CreateOrder(orderItem);

                        subOrder2.Items.Add(
                            orderFactory.CreateOrderItem(orderItem));

                        yield return subOrder2;
                    }
                    else */ if (orderItem.Subscription != null)
                    {
                        // Subscription Item with multiple Deliveries

                        var dates = orderDateGenerator.GetOrderDatesFromSubscription(orderItem.Subscription, startDate, endDate);

                        foreach (var date in dates)
                        {
                            var subOrder2 = orderFactory
                                .CreateOrder(orderItem)
                                .SetPlannedDates(date.Start, date.End);

                            subOrder2.Items.Add(
                                orderFactory.CreateOrderItem(orderItem));

                            yield return subOrder2;
                        }
                    }
                    /*
                    else
                    {
                        // Item part of Main Delivery

                        var targetOrderItem = orderFactory.CreateOrderItem(orderItem);

                        subOrder.Items.Add(targetOrderItem);
                    }
                    */
                }

                // Return Main Delivery if any items
                if (subOrder.Items.Any())
                {
                    yield return subOrder;
                }
            }
            else
            {
                // Subscription Order

                var dates = orderDateGenerator.GetOrderDatesFromSubscription(order.Subscription, startDate, endDate);

                foreach (var date in dates)
                {
                    var subOrder = orderFactory.CreateOrder(order)
                        .SetPlannedDates(date.Start, date.End);

                    foreach (var orderItem in order.Items)
                    {
                        if (orderItem.Subscription != null)
                        {
                            throw new Exception($"Nested subscriptions not allowed (OrderItem.Id: {orderItem.Id})");
                        }

                        if (orderItem.HasDeliveryDetails)
                        {
                            // Deliver to separate address. Its own Delivery.

                            var subOrder2 = orderFactory
                              .CreateOrder(orderItem)
                              .SetPlannedDates(date.Start, date.End);

                            subOrder2.Items.Add(
                                orderFactory.CreateOrderItem(orderItem));

                            yield return subOrder2;
                        }
                        else
                        {
                            // Item part of Main Delivery

                            var targetOrderItem = orderFactory.CreateOrderItem(orderItem);

                            subOrder.Items.Add(targetOrderItem);
                        }
                    }

                    // Return Main Delivery if any items
                    if (subOrder.Items.Any())
                    {
                        yield return subOrder;
                    }
                }
            }
        }
    }
}