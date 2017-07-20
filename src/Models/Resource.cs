using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
