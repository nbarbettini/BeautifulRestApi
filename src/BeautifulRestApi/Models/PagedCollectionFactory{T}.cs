using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<PagedCollection<T>> CreateFrom(IQueryable<T> queryable, int offset, int limit)
        {
            var count = await queryable.CountAsync();

            var items = await queryable
                .Skip(offset)
                .Take(limit)
                .ToArrayAsync();

            return new PagedCollection<T>()
            {
                Meta = PlaceholderLink.ToCollection(_endpoint),
                Items = items,
                First = PlaceholderLink.ToCollection(_endpoint),
                Last = GetLastLink(count, limit),
                Next = GetNextLink(count, offset, limit),
                Previous = GetPreviousLink(count, offset, limit),
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        private ILink GetLastLink(int size, int limit)
        {
            if (size <= limit)
            {
                return PlaceholderLink.ToCollection(_endpoint);
            }

            var routeValues = new
            {
                limit,
                offset = Math.Ceiling((size - (double) limit)/limit) * limit
            };

            return PlaceholderLink.ToCollection(_endpoint, values: routeValues);
        }

        private ILink GetNextLink(int size, int offset, int limit)
        {
            var nextPage = offset + limit;

            if (nextPage >= size)
            {
                return null;
            }

            var routeValues = new
            {
                limit,
                offset = nextPage
            };

            return PlaceholderLink.ToCollection(_endpoint, values: routeValues);
        }

        private ILink GetPreviousLink(int size, int offset, int limit)
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

            if (previousPage <= 0)
            {
                return PlaceholderLink.ToCollection(_endpoint);
            }

            var routeValues = new
            {
                limit,
                offset = previousPage
            };

            return PlaceholderLink.ToCollection(_endpoint, values: routeValues);
        }
    }
}
