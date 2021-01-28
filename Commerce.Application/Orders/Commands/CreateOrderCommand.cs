using Commerce.Domain.Entities;
using Commerce.Application.Orders.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Commerce.Application.Common.Interfaces;

namespace Commerce.Application.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Order>
    {
        class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
        {
            private readonly IApplicationDbContext applicationDbContext;
            private readonly IMediator mediator;

            public CreateOrderCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator)
            {
                this.applicationDbContext = applicationDbContext;
                this.mediator = mediator;
            }

            public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = new Order();

                applicationDbContext.Orders.Add(order);

                await applicationDbContext.SaveChangesAsync();

                await mediator.Publish(new OrderCreatedNotification(order.Id));

                return order;
            }
        }
    }
}
