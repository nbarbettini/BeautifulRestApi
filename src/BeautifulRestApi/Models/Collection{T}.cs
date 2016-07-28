using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class Collection<T> : Resource
    {
        public Collection(Link meta, IEnumerable<T> items)
        {
            Meta = meta;
            Items = items.ToArray();
        }

        public T[] Items { get; set; }
    }
}
