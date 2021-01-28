using Commerce.Application.Orders.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Commerce.Application.Common.Interfaces;

namespace Commerce.Application.Orders.Commands
{
    public class CancelOrderCommand : IRequest
    {
        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }

        class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
        {
            private readonly IApplicationDbContext applicationDbContext;
            private readonly IMediator mediator;

            public CancelOrderCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator)
            {
                this.applicationDbContext = applicationDbContext;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
            {
                //var order = new Order();

                //applicationDbContext.Orders.Add(order);

                //await applicationDbContext.SaveChangesAsync();

                await mediator.Publish(new OrderCanceledNotification(request.OrderId));

                return Unit.Value;
            }
        }
    }
}
