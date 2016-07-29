using System;
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

        public ILink Rewrite(ILink original)
        {
            if (original == null)
            {
                return null;
            }

            string href = null;
            string method = null;
            string[] relations = null;

            var asPlaceholderLink = original as PlaceholderLink;
            if (asPlaceholderLink != null)
            {
                var routeValues = new RouteValueDictionary(asPlaceholderLink.Values)
                {
                    {"controller", original.Href}
                };

                href = _urlHelper.Link("default", routeValues);
            }
            else
            {
                href = original.Href;
            }

            if (!original.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                method = original.Method;
            }

            if (original.Relations?.Length > 0)
            {
                relations = original.Relations;
            }

            return new Link { Href = href, Method = method, Relations = relations };
        }
    }
}
