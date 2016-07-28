using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BeautifulRestApi.Filters
{
    public class ResourceEnricher : AbstractResultEnricher<Resource>
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public ResourceEnricher(IUrlHelperFactory urlHelper)
        {
            _urlHelperFactory = urlHelper;
        }

        protected override void OnEnriching(ResultExecutingContext context, Resource result)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(context);

            var href = string.Empty;
            var asResourceLink = result.Meta as ResourceLink;

            if (asResourceLink != null)
            {
                href = urlHelper.Link("default", new {controller = asResourceLink.Href, id = asResourceLink.Id});
            }
            else
            {
                href = urlHelper.Link("default", new {controller = result.Meta.Href});
            }

            result.Meta = new Link(href, result.Meta.Relations, result.Meta.Method);
        }
    }
}
