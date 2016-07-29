using System;
using BeautifulRestApi.Models;
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

        protected override void OnEnriching(ResultExecutingContext context, Resource result, Action<ResultExecutingContext, object> enrichAction)
        {
            var linkRewriter = new LinkRewriter(_urlHelperFactory.GetUrlHelper(context));

            result.Meta = linkRewriter.Rewrite(result.Meta);

            if (result.Forms != null)
            {
                foreach (var form in result.Forms)
                {
                    enrichAction(context, form);
                }
            }
        }
    }
}
