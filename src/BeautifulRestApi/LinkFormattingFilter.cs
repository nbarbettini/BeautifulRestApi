using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BeautifulRestApi
{
    public class LinkFormattingFilter : IAsyncResultFilter
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public LinkFormattingFilter(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var asObjectResult = context.Result as ObjectResult;
            if (asObjectResult == null)
            {
                return;
            }

            var urlHelper = _urlHelperFactory.GetUrlHelper(context);
            EnrichLinks(asObjectResult.Value, urlHelper);

            await next();
        }

        private static void EnrichLinks(object input, IUrlHelper urlHelper)
        {
            foreach (var prop in input.GetType().GetTypeInfo().DeclaredProperties)
            {
                var value = prop.GetValue(input);
                if (value == null)
                {
                    continue;
                }

                if (value.GetType() == typeof(ResourceLink))
                {
                    prop.SetValue(input, new LiteralLink(urlHelper.Action("get", new { id = (value as ResourceLink).Id })));
                    continue;
                }

                if (value.GetType() == typeof(CollectionLink))
                {
                    prop.SetValue(input, new LiteralLink(urlHelper.Action("get", (value as CollectionLink).Href)));
                    continue;
                }

                if (prop.PropertyType.GetTypeInfo().IsClass)
                {
                    EnrichLinks(prop.GetValue(input), urlHelper);
                    continue;
                }
            }
        }
    }
}
