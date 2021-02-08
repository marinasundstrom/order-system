using System.Collections.Generic;

namespace Commerce.Application.Common
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> items, int count, int totalCount, int page, int totalPages)
        {
            Items = items;
            Count = count;
            TotalCount = totalCount;
            Page = page;
            TotalPages = totalPages;
        }

        public IEnumerable<T> Items { get; }

        public int Count { get; }

        public int TotalCount { get; }

        public int Page { get; }

        public int TotalPages { get; }
    }
}
