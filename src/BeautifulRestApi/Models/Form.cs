using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class Form
    {
        public Form(string href, string method, IEnumerable<FormField> fields)
        {
            Meta = new Link(href, method: method, relations: new[] {"create-form "});

            Items = fields.ToArray();
        }

        [JsonProperty(Order = -2)]
        public Link Meta { get; set; }

        public FormField[] Items { get; set; }
    }
}
