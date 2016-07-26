using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BeautifulRestApi.Models
{
    public abstract class Resource
    {
        [JsonProperty(Order = -2)]
        public Metadata Meta { get; set; }
    }
}
