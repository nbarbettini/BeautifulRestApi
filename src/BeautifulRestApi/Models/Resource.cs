using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BeautifulRestApi.Models
{
    public abstract class Resource
    {
        protected Resource(string href)
        {
            Meta.Href = href;
        }

        [JsonProperty(Order = -2)]
        public Metadata Meta { get; set; } = new Metadata();
    }
}
