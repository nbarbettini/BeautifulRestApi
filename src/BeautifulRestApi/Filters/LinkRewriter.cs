using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BeautifulRestApi.Filters
{
    public class LinkRewriter
    {
        private readonly IUrlHelper _urlHelper;

        public LinkRewriter(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public Link Rewrite(Link original)
        {
            if (original == null)
            {
                return null;
            }

            var asResourceLink = original as ResourceLink;
            if (asResourceLink != null)
            {
                var href = _urlHelper.Link("default", new { controller = asResourceLink.Href, id = asResourceLink.Id });
                return new Link(href, original.Relations, original.Method);
            }

            var asCollectionLink = original as CollectionLink;
            if (asCollectionLink != null)
            {
                var extendedValues = new RouteValueDictionary(asCollectionLink.Values)
                {
                    { "controller", original.Href }
                };

                var href = _urlHelper.Link("default", extendedValues);
                return new Link(href, original.Relations, original.Method);
            }

            return original;
        }
    }
}
