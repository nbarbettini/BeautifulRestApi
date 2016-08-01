using System.Linq;
using Microsoft.AspNetCore.Routing;

namespace BeautifulRestApi.Models
{
    public class PlaceholderLink : ILink
    {
        public string Href { get; set; }

        public string[] Relations { get; set; }

        public string Method { get; set; }

        public RouteValueDictionary Values { get;  set; }

        public PlaceholderLink()
        { }

        public PlaceholderLink(ILink existing)
        {
            Href = existing.Href;
            Relations = existing.Relations.ToArray();
            Method = existing.Method;

            var asPlaceholder = existing as PlaceholderLink;
            if (asPlaceholder != null)
            {
                Values = new RouteValueDictionary(asPlaceholder.Values);
            }
        }

        public static PlaceholderLink ToResource(string controller, string id, string method = "GET", object values = null)
            => new PlaceholderLink
            {
                Href = controller,
                Method = method,
                Relations = new string[0],
                Values = new RouteValueDictionary(values)
                {
                    {"id", id}
                }
            };

        public static PlaceholderLink ToCollection(string controller, string method = "GET", object values = null)
            => new PlaceholderLink
            {
                Href = controller,
                Method = method,
                Relations = new []{"collection"},
                Values = new RouteValueDictionary(values)
            };
    }
}
