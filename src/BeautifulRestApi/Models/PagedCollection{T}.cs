using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BeautifulRestApi.Models
{
    public abstract class PagedCollection : Resource
    {
        protected PagedCollection(Link meta)
        {
            Meta = meta;
        }

        public abstract IEnumerator GetEnumerator();

        public Link First { get; set; }

        public Link Previous { get; set; }

        public Link Next { get; set; }

        public Link Last { get; set; }
    }

    public class PagedCollection<T> : PagedCollection
    {
        public PagedCollection(Link meta, IEnumerable<T> items)
            : base(meta)
        {
            Items = items.ToArray();
        }

        public T[] Items { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public int Size { get; set; }

        public override IEnumerator GetEnumerator() => Items.GetEnumerator();
    }
}
