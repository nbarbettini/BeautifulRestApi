using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class LiteralLink : Link
    {
        [JsonProperty(Order = -2)]
        public Metadata Meta { get; set; } = new Metadata();

        public LiteralLink(string href, string relation = null)
            : base(href)
        {
            Meta.Href = href;

            if (!string.IsNullOrEmpty(relation))
            {
                Meta.Relations = new[] {relation};
            }
        }
    }
}
