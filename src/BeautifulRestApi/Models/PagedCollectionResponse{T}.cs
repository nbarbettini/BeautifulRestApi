using System.Collections.Generic;

namespace BeautifulRestApi.Models
{
    public class PagedCollectionResponse<T> : CollectionResponse<T>
    {
        public PagedCollectionResponse(string collectionHref, IEnumerable<T> items)
            : base(collectionHref, items)
        {
        }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public int Size { get; set; }

        public Link First { get; set; }

        public Link Previous { get; set; }

        public Link Next { get; set; }

        public Link Last { get; set; }
    }
}
