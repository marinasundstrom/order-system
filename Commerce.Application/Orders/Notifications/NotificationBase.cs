using MediatR;
using System;

namespace Commerce.Application.Orders.Notifications
{
    public class NotificationBase : INotification
    {
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
    }
}
