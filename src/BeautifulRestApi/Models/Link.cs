using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class Link
    {
        public Link(string href, string[] relations = null, string method = null)
        {
            Href = href;
            Relations = relations;
            Method = method;
        }

        public string Href { get; }

        [JsonProperty(PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Method { get; }
    }
}
