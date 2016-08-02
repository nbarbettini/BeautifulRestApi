using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Models
{
    public class PagedCollectionFactory<T>
    {
        private readonly ILink _meta;

        public PagedCollectionFactory(ILink meta)
        {
            _meta = meta;
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
                Meta = _meta,
                Items = items,
                First = GetFirstLink(),
                Last = GetLastLink(count, limit),
                Next = GetNextLink(count, offset, limit),
                Previous = GetPreviousLink(count, offset, limit),
                Limit = limit,
                Offset = offset,
                Size = count
            };
        }

        private ILink GetFirstLink()
            => new PlaceholderLink(_meta);

        private ILink GetLastLink(int size, int limit)
        {
            if (size <= limit)
            {
                return GetFirstLink();
            }

            var offset = Math.Ceiling((size - (double) limit)/limit)*limit;

            var newLink = new PlaceholderLink(_meta);
            newLink.Values.Add("limit", limit);
            newLink.Values.Add("offset", offset);

            return newLink;
        }

        private ILink GetNextLink(int size, int offset, int limit)
        {
            var nextPage = offset + limit;

            if (nextPage >= size)
            {
                return null;
            }

            var newLink = new PlaceholderLink(_meta);
            newLink.Values.Add("limit", limit);
            newLink.Values.Add("offset", nextPage);

            return newLink;
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
                return GetFirstLink();
            }

            var newLink = new PlaceholderLink(_meta);
            newLink.Values.Add("limit", limit);
            newLink.Values.Add("offset", previousPage);

            return newLink;
        }
    }
}
