using Newtonsoft.Json;

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

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Form[] Forms { get; set; }
    }
}
