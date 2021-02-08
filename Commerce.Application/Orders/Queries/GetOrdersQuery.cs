using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Commerce.Application.Common;
using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Application.Orders.Queries
{
    public class GetOrdersQuery : IRequest<PagedResult<OrderDto>>
    {
        public GetOrdersQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; } = 1;

        public int PageSize { get; } = 50;

        public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PagedResult<OrderDto>>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetOrdersQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<PagedResult<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
            {
                var query = applicationDbContext.Orders
                    .Include(x => x.Subscription)
                    .OrderBy(x => x.OrderDate)
                    .AsSplitQuery()
                    .AsNoTracking()
                    .AsQueryable();

                return await query.CreatePagedResultAsync(
                    request.Page, request.PageSize, o => new OrderDto(o), cancellationToken);
            }
        }
    }
}
