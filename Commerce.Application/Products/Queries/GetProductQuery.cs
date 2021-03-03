using Commerce.Application.Common.Interfaces;
using Commerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Commerce.Application.Products.Queries
{
    public class GetProductQuery : IRequest<Product>
    {
        public GetProductQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
        {
            private readonly IApplicationDbContext applicationDbContext;

            public GetProductQueryHandler(IApplicationDbContext applicationDbContext)
            {
                this.applicationDbContext = applicationDbContext;
            }

            public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                return await applicationDbContext.Products
                    .AsNoTracking()
                    .FirstAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            }
        }
    }
}
