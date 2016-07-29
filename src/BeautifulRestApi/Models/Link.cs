using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class Link : ILink
    {
        public string Href { get; set; }

        public string[] Relations { get; set; }

        public string Method { get; set; }
    }
}
