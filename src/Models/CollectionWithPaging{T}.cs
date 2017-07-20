using System;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class CollectionWithPaging<T> : Collection<T>
    {
        /// <summary>
        /// Gets or sets the offset of the current page.
        /// </summary>
        /// <value>The offset of the current page.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        /// <summary>
        /// Gets or sets the limit of the current paging options.
        /// </summary>
        /// <value>The limit of the current paging options.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        /// <summary>
        /// Gets or sets the total size of the collection (irrespective of any paging options).
        /// </summary>
        /// <value>The total size of the collection.</value>
        public long Size { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link First { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Previous { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Next { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Last { get; set; }

        public static CollectionWithPaging<T> Create(RouteLink self, T[] items, long size, PagingOptions pagingOptions)
            => Create<CollectionWithPaging<T>>(self, items, size, pagingOptions);

        public static TResponse Create<TResponse>(RouteLink self, T[] items, long size, PagingOptions pagingOptions)
            where TResponse : CollectionWithPaging<T>, new()
            => new TResponse
            {
                Self = self,
                Value = items,
                Size = size,
                Offset = pagingOptions.Offset,
                Limit = pagingOptions.Limit,
                First = self,
                Next = GetNextLink(self, size, pagingOptions),
                Previous = GetPreviousLink(self, size, pagingOptions),
                Last = GetLastLink(self, size, pagingOptions)
            };

        private static Link GetNextLink(RouteLink self, long size, PagingOptions pagingOptions)
        {
            if (pagingOptions?.Limit == null) return null;
            if (pagingOptions?.Offset == null) return null;

            var limit = pagingOptions.Limit.Value;
            var offset = pagingOptions.Offset.Value;

            var next = offset + limit;
            if (next >= size) return null;

            var parameters = new RouteValueDictionary(self.RouteValues)
            {
                ["limit"] = limit,
                ["offset"] = next
            };

            var newLink = ToCollection(self.RouteName, parameters);
            return newLink;
        }

        private static Link GetLastLink(RouteLink self, long size, PagingOptions pagingOptions)
        {
            if (pagingOptions?.Limit == null) return null;

            var limit = pagingOptions.Limit.Value;

            if (size <= limit) return null;

            var offset = Math.Ceiling((size - (double)limit) / limit) * limit;

            var parameters = new RouteValueDictionary(self.RouteValues)
            {
                ["limit"] = limit,
                ["offset"] = offset
            };
            var newLink = ToCollection(self.RouteName, parameters);

            return newLink;
        }

        private static Link GetPreviousLink(RouteLink self, long size, PagingOptions pagingOptions)
        {
            if (pagingOptions?.Limit == null) return null;
            if (pagingOptions?.Offset == null) return null;

            var limit = pagingOptions.Limit.Value;
            var offset = pagingOptions.Offset.Value;

            if (offset == 0)
            {
                return null;
            }

            if (offset > size)
            {
                return GetLastLink(self, size, pagingOptions);
            }

            var previousPage = Math.Max(offset - limit, 0);

            if (previousPage <= 0)
            {
                return self;
            }

            var parameters = new RouteValueDictionary(self.RouteValues)
            {
                ["limit"] = limit,
                ["offset"] = previousPage
            };
            var newLink = Link.ToCollection(self.RouteName, parameters);

            return newLink;
        }
    }
}
