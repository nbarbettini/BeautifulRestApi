using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class Metadata
    {
        public string Href { get; set; }

        [JsonProperty("rel", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }
    }
}
