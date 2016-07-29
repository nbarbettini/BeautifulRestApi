using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public abstract class Resource
    {
        [JsonProperty(Order = -2)]
        public ILink Meta { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Form[] Forms { get; set; }
    }
}
