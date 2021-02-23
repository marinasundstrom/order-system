using Commerce.Application.Common.Interfaces;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commerce.Application.Orders.Notifications
{
    public class OrderNotificationHandler
        : INotificationHandler<OrderCreatedNotification>, INotificationHandler<OrderCanceledNotification>
    {
        private readonly IApplicationDbContext applicationDbContext;
        //private readonly IBackgroundJobClient backgroundJobClient;
        //private readonly IRecurringJobManager recurringJobManager;
        private readonly ILogger<OrderNotificationHandler> logger;

        public OrderNotificationHandler(
            IApplicationDbContext applicationDbContext,
            //IBackgroundJobClient backgroundJobClient,
            //IRecurringJobManager recurringJobManager,
            ILogger<OrderNotificationHandler> logger)
        {
            this.applicationDbContext = applicationDbContext;
            //this.backgroundJobClient = backgroundJobClient;
            //this.recurringJobManager = recurringJobManager;
            this.logger = logger;
        }

        public Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Order created: {notification.OrderId} at {notification.SendDate}");

            // Run scripts

            //var jobId = backgroundJobClient.Enqueue(() => Console.WriteLine("Job was enqued."));

            //recurringJobManager.AddOrUpdate("42", () => Console.WriteLine("Hej Sven!."), Cron.MinuteInterval(1));

            //recurringJobManager.Trigger("42");

            // Send an email to person
            //var jobId3 = backgroundJobClient.Enqueue<IEmailService>((emailSevice) => emailSevice.SendEmailAsync("", "", ""));

            return Task.CompletedTask;
        }

        public Task Handle(OrderCanceledNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Order canceled: {notification.OrderId} at {notification.SendDate}");

            return Task.CompletedTask;
        }
    }
}
