using System;
using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BeautifulRestApi.Filters
{
    public class CollectionEnricher : AbstractResultEnricher<Collection>
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public CollectionEnricher(IUrlHelperFactory urlHelper)
        {
            _urlHelperFactory = urlHelper;
        }

        protected override void OnEnriching(ResultExecutingContext context, Collection<T> result, Action<ResultExecutingContext, object> enrichChildAction)
        {
            var linkRewriter = new LinkRewriter(_urlHelperFactory.GetUrlHelper(context));

            // foreach item in items

            // need some way to call the parent filter on a new item
        }
    }
}
