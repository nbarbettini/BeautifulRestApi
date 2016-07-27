using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class CollectionResponse<T> : Resource
    {
        public CollectionResponse(Link href, IEnumerable<T> items)
        {
            Href = href;
            Items = items.ToArray();
        }

        public T[] Items { get; set; }
    }
}
