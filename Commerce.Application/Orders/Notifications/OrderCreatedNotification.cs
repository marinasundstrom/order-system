namespace Commerce.Application.Orders.Notifications
{
    public class OrderCreatedNotification : NotificationBase
    {
        public OrderCreatedNotification(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}
