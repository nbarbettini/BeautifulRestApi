using System;
using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BeautifulRestApi.Filters
{
    public class PagedCollectionEnricher : AbstractResultEnricher<PagedCollection>
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public PagedCollectionEnricher(IUrlHelperFactory urlHelper)
        {
            _urlHelperFactory = urlHelper;
        }

        protected override void OnEnriching(
            ResultExecutingContext context,
            PagedCollection result,
            Action<ResultExecutingContext, object> enrichChildAction)
        {
            var linkRewriter = new LinkRewriter(_urlHelperFactory.GetUrlHelper(context));

            result.First = linkRewriter.Rewrite(result.First);
            result.Last = linkRewriter.Rewrite(result.Last);
            result.Next = linkRewriter.Rewrite(result.Next);
            result.Previous = linkRewriter.Rewrite(result.Previous);
        }
    }
}
