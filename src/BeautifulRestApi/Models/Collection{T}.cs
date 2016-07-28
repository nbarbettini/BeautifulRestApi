using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BeautifulRestApi.Models
{
    public abstract class Collection : Resource
    {
        protected Collection(Link meta)
        {
            Meta = meta;
        }

        public abstract IEnumerator GetEnumerator();
    }

    public class Collection<T> : Collection
    {
        public Collection(Link meta, IEnumerable<T> items)
            : base(meta)
        {
            Items = items.ToArray();
        }

        public T[] Items { get; set; }

        public override IEnumerator GetEnumerator() => Items.GetEnumerator();
    }
}
