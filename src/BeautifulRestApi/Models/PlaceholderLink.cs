using Microsoft.AspNetCore.Routing;

namespace BeautifulRestApi.Models
{
    public class PlaceholderLink : ILink
    {
        public string Href { get; set; }

        public string[] Relations { get; set; }

        public string Method { get; set; }

        public RouteValueDictionary Values { get;  set; }

        public static PlaceholderLink ToResource(string route, string id, string method = "GET", object values = null)
            => new PlaceholderLink
            {
                Href = route,
                Method = method,
                Relations = new string[0],
                Values = new RouteValueDictionary(values)
                {
                    {"id", id}
                }
            };

        public static PlaceholderLink ToCollection(string route, string method = "GET", object values = null)
            => new PlaceholderLink
            {
                Href = route,
                Method = method,
                Relations = new []{"collection"},
                Values = new RouteValueDictionary(values)
            };
    }
}
