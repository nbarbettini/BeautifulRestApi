using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class CollectionResponse<T> : Resource
    {
        public CollectionResponse(string collectionHref, IEnumerable<T> items)
            : base(collectionHref)
        {
            Meta.Relations = new[] {"collection"};
            Items = items.ToArray();
        }

        public T[] Items { get; set; }
    }
}
