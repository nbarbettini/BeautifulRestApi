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
        private readonly string _baseHref;

        public PagedCollectionFactory(string baseHref)
        {
            _baseHref = baseHref;
        }

        public async Task<PagedCollectionResponse<TResult>> CreateFrom<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, TResult>> selector, int offset, int limit)
        {
            var count = await queryable.CountAsync();

            var items = await queryable
                .Skip(offset)
                .Take(limit)
                .Select(selector)
                .ToArrayAsync();

            return new PagedCollectionResponse<TResult>(_baseHref, items)
            {
                First = new Link(_baseHref, relation: "collection"),
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
            var href = size > limit
                ? $"{_baseHref}?offset={Math.Floor((size - (double)limit) / limit) * limit}"
                : _baseHref;

            return new Link(href, relation: "collection");
        }

        private Link GetNextLink(int size, int offset, int limit)
        {
            var nextPage = offset + limit;

            return nextPage >= size
                ? null 
                : new Link($"{_baseHref}?offset={nextPage}", relation: "collection");
        }

        private Link GetPreviousLink(int size, int offset, int limit)
        {
            if (offset == 0)
            {
                return null;
            }

            var previousPage = Math.Max(offset - limit, 0);

            var href = previousPage > 0
                ? $"{_baseHref}?offset={previousPage}"
                : _baseHref;

            return new Link(href, relation: "collection");
        }
    }
}
