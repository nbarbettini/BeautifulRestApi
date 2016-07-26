using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class CollectionResponse<T> : Resource
    {
        public CollectionResponse(string collectionhref, IEnumerable<T> items)
            : base(collectionhref)
        {
            Meta.Relations = new[] {"collection"};
            Items = items.ToArray();
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public T[] Items { get; set; }
    }
}
