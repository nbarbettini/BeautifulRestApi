using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class CollectionResponse<T>
    {
        public CollectionResponse(Link href, IEnumerable<T> items)
        {
            Meta = href;
            Items = items.ToArray();
        }

        [JsonProperty(Order = -2)]
        public Link Meta { get; }

        public T[] Items { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Form[] Forms { get; set; }
    }
}
