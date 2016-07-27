using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class Form
    {
        public Form(string href, string method, IEnumerable<FormField> fields)
        {
            Meta = new Metadata
            {
                Href = href,
                Method = method,
                Relations = new[] {"create-form"}
            };

            Items = fields.ToArray();
        }

        [JsonProperty(Order = -2)]
        public Metadata Meta { get; set; }

        public FormField[] Items { get; set; }
    }
}
