using Microsoft.AspNetCore.Routing;

namespace BeautifulRestApi.Models
{
    public class CollectionLink : Link
    {
        public CollectionLink(string path, RouteValueDictionary values = null)
            : base(path, new[] {"collection"})
        {
            Values = values;
        }

        public RouteValueDictionary Values { get; }
    }
}
