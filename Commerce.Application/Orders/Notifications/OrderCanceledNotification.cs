namespace Commerce.Application.Orders.Notifications
{
    public class OrderCanceledNotification : NotificationBase
    {
        public OrderCanceledNotification(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}
