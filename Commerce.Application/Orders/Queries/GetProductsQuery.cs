using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Commerce.Orders.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
        {
            private readonly IApplicationDbContext IApplicationDbContext;

            public GetProductsQueryHandler(IApplicationDbContext IApplicationDbContext)
            {
                this.IApplicationDbContext = IApplicationDbContext;
            }

            public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                return await IApplicationDbContext.Products.ToArrayAsync();
            }
        }
    }
}
