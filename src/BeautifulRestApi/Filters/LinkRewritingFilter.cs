using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BeautifulRestApi.Filters
{
    public class LinkRewritingFilter : IAsyncResultFilter
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public LinkRewritingFilter(IUrlHelperFactory urlHelperFactory)
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

            var rewriter = new LinkRewriter(_urlHelperFactory.GetUrlHelper(context));

            RewriteLinks(asObjectResult.Value, rewriter);

            await next();
        }

        private static void RewriteLinks(object input, LinkRewriter rewriter)
        {
            var allProperties = input.GetType().GetTypeInfo().GetAllProperties().ToArray();

            foreach (var linkProperty in allProperties.Where(p => p.PropertyType == typeof(Link)))
            {
                var rewritten = rewriter.Rewrite(linkProperty.GetValue(input) as Link);

                if (rewritten != null)
                {
                    linkProperty.SetValue(input, rewritten);
                }
            }

            foreach (var arrayProperty in allProperties.Where(p => p.PropertyType.IsArray))
            {
                var array = arrayProperty.GetValue(input) as Array ?? new Array[0];

                foreach (var element in array)
                {
                    RewriteLinks(element, rewriter);
                }
            }
        }
    }
}