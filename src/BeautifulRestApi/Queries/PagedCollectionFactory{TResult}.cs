using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Queries
{
    public class PagedCollectionFactory<TResult>
    {
        private readonly string _endpoint;

        public PagedCollectionFactory(string endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task<PagedCollectionResponse<TResult>> CreateFrom<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, TResult>> selector, int offset, int limit)
        {
            var count = await queryable.CountAsync();

            var items = await queryable
                .Skip(offset)
                .Take(limit)
                .Select(selector)
                .ToArrayAsync();

            return new PagedCollectionResponse<TResult>(new CollectionLink(_endpoint), items)
            {
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
                ? new CollectionLink(_endpoint, $"offset={Math.Floor((size - (double) limit)/limit)*limit}")
                : new CollectionLink(_endpoint);
        }

        private Link GetNextLink(int size, int offset, int limit)
        {
            var nextPage = offset + limit;

            return nextPage >= size
                ? null 
                : new CollectionLink(_endpoint, $"offset={nextPage}");
        }

        private Link GetPreviousLink(int size, int offset, int limit)
        {
            if (offset == 0)
            {
                return null;
            }

            var previousPage = Math.Max(offset - limit, 0);

            return previousPage > 0
                ? new CollectionLink(_endpoint, $"offset={previousPage}")
                : new CollectionLink(_endpoint);
        }
    }
}
