using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Commerce.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetProductsQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.Products.ToArrayAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
