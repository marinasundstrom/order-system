using Commerce.Application.Deliveries;
using Commerce.Domain.Entities;
using Commerce.Application.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commerce.Application.Orders
{
    public class OrderDeliveryGenerator
    {
        private DeliveryFactory deliveryFactory;
        private readonly SubscriptionDeliveryDatesGenerator deliveryDatesGenerator;

        public OrderDeliveryGenerator(
            DeliveryFactory deliveryFactory,
            SubscriptionDeliveryDatesGenerator deliveryDatesGenerator)
        {
            this.deliveryFactory = deliveryFactory;
            this.deliveryDatesGenerator = deliveryDatesGenerator;
        }

        public IEnumerable<Delivery> GetDeliveries(Order order, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (order.Subscription == null)
            {
                // Order (Non-Subscription Order)

                var delivery = deliveryFactory.CreateDelivery(order);
                //.SetDeliveryDate(date.Start, date.End);

                foreach (var orderItem in order.Items)
                {
                    if (orderItem.Subscription == null && orderItem.HasDeliveryDetails)
                    {
                        // Deliver to separate address. Its own Delivery.

                        var subDelivery = deliveryFactory.CreateDelivery(orderItem);

                        subDelivery.Items.Add(
                            deliveryFactory.CreateDeliveryItem(orderItem));

                        yield return subDelivery;
                    }
                    else if (orderItem.Subscription != null)
                    {
                        // Subscription Item with multiple Deliveries

                        var dates = deliveryDatesGenerator.GetDeliveryDatesFromSubscription(orderItem.Subscription, startDate, endDate);

                        foreach (var date in dates)
                        {
                            var subDelivery = deliveryFactory
                                .CreateDelivery(orderItem)
                                .SetDeliveryDate(date.Start, date.End);

                            subDelivery.Items.Add(
                                deliveryFactory.CreateDeliveryItem(orderItem));

                            yield return subDelivery;
                        }
                    }
                    else
                    {
                        // Item part of Main Delivery

                        var deliveryItem = deliveryFactory.CreateDeliveryItem(orderItem);

                        delivery.Items.Add(deliveryItem);
                    }
                }

                // Return Main Delivery if any items
                if (delivery.Items.Any())
                {
                    yield return delivery;
                }
            }
            else
            {
                // Subscription Order

                var dates = deliveryDatesGenerator.GetDeliveryDatesFromSubscription(order.Subscription, startDate, endDate);

                foreach (var date in dates)
                {
                    var delivery = deliveryFactory.CreateDelivery(order)
                        .SetDeliveryDate(date.Start, date.End);

                    foreach (var orderItem in order.Items)
                    {
                        if (orderItem.Subscription != null)
                        {
                            throw new Exception($"Nested subscriptions not allowed (OrderItem.Id: {orderItem.Id})");
                        }

                        if (orderItem.HasDeliveryDetails)
                        {
                            // Deliver to separate address. Its own Delivery.

                            var subDelivery = deliveryFactory
                              .CreateDelivery(orderItem)
                              .SetDeliveryDate(date.Start, date.End);

                            subDelivery.Items.Add(
                                deliveryFactory.CreateDeliveryItem(orderItem));

                            yield return subDelivery;
                        }
                        else
                        {
                            // Item part of Main Delivery

                            var deliveryItem = deliveryFactory.CreateDeliveryItem(orderItem);

                            delivery.Items.Add(deliveryItem);
                        }
                    }

                    // Return Main Delivery if any items
                    if (delivery.Items.Any())
                    {
                        yield return delivery;
                    }
                }
            }
        }
    }
}