using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BeautifulRestApi.Models
{
    public interface ICollection
    {
        IEnumerator GetEnumerator();
    }

    public class Collection<T> : Resource, ICollection
    {
        public Collection(Link meta, IEnumerable<T> items)
        {
            Meta = meta;
            Items = items.ToArray();
        }

        public T[] Items { get; set; }

        public IEnumerator GetEnumerator() => Items.GetEnumerator();
    }
}
