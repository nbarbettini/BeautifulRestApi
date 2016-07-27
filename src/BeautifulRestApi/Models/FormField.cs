using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class FormField
    {
        [JsonProperty(PropertyName = "minlength", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinLength { get; set; }

        [JsonProperty(PropertyName = "maxlength", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxLength { get; set; }

        public string Name { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Required { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
