using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public interface ILink
    {
        string Href { get; set; }

        [JsonProperty(PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
        string[] Relations { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string Method { get; set; }
    }
}
