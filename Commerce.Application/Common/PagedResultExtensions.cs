using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Application.Common
{
    public static class PagedResultExtensions
    {
        public static async Task<PagedResult<T>> CreatePagedResultAsync<T>(this IQueryable<T> query, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            int totalCount = await query.CountAsync();

            int totalPages = GetTotalPages(pageSize, totalCount);
            query = GetItems(query, page, pageSize);

            int count = await query.CountAsync();

            var items = await query.ToArrayAsync(cancellationToken);

            return new PagedResult<T>(
                items,
                count,
                totalCount,
                page,
                totalPages);
        }

        public static async Task<PagedResult<T>> CreatePagedResultAsync<TInput, T>(this IQueryable<TInput> query, int page, int pageSize, Func<TInput, T> mapper, CancellationToken cancellationToken = default)
        {
            int totalCount = await query.CountAsync();

            int totalPages = GetTotalPages(pageSize, totalCount);

            query = GetItems(query, page, pageSize);

            int count = await query.CountAsync();

            var items = await query.ToArrayAsync(cancellationToken);

            return new PagedResult<T>(
                items.Select(mapper),
                count,
                totalCount,
                page,
                totalPages);
        }

        private static int GetTotalPages(int pageSize, int totalCount) =>
                (int)Math.Ceiling((double)totalCount / pageSize);

        private static IQueryable<TInput> GetItems<TInput>(IQueryable<TInput> query, int page, int pageSize) =>
                query.Skip((page - 1) * pageSize)
                     .Take(pageSize);
    }
}
