using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Models
{
    public class PagedCollectionFactory<T>
    {
        private readonly string _endpoint;

        public PagedCollectionFactory(string endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task<PagedCollection<T>> CreateFrom<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, T>> selector, int offset, int limit)
        {
            var count = await queryable.CountAsync();

            var items = await queryable
                .Skip(offset)
                .Take(limit)
                .Select(selector)
                .ToArrayAsync();

            return new PagedCollection<T>()
            {
                Meta = new CollectionLink(_endpoint),
                Items = items,
                First = new CollectionLink(_endpoint),
                Last = GetLastLink(count, limit),
                Next = GetNextLink(count, offset, limit),
                Previous = GetPreviousLink(count, offset, limit),
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        private Link GetLastLink(int size, int limit)
        {
            return size > limit
                ? new CollectionLink(_endpoint, new RouteValueDictionary(new { limit, offset = Math.Ceiling((size - (double)limit) / limit) * limit }))
                : new CollectionLink(_endpoint);
        }

        private Link GetNextLink(int size, int offset, int limit)
        {
            var nextPage = offset + limit;

            return nextPage < size
                ? new CollectionLink(_endpoint, new RouteValueDictionary(new {limit, offset = nextPage}))
                : null;
        }

        private Link GetPreviousLink(int size, int offset, int limit)
        {
            if (offset == 0)
            {
                return null;
            }

            if (offset > size)
            {
                return GetLastLink(size, limit);
            }

            var previousPage = Math.Max(offset - limit, 0);

            return previousPage > 0
                ? new CollectionLink(_endpoint, new RouteValueDictionary(new {limit, offset = previousPage}))
                : new CollectionLink(_endpoint);
        }
    }
}
