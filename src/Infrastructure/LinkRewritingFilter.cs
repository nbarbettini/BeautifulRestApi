using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BeautifulRestApi.Infrastructure
{
    public sealed class LinkRewritingFilter : IAsyncResultFilter
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public LinkRewritingFilter(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            var asObjectResult = context.Result as ObjectResult;

            var shouldSkip = asObjectResult?.Value == null || asObjectResult.StatusCode != (int)HttpStatusCode.OK;
            if (shouldSkip)
            {
                await next();
                return;
            }

            var rewriter = new LinkRewriter(_urlHelperFactory.GetUrlHelper(context));
            RewriteAllLinks(asObjectResult.Value, rewriter);

            await next();
        }

        private static void RewriteAllLinks(object model, LinkRewriter rewriter)
        {
            if (model == null) return;

            var allProperties = model
                .GetType().GetTypeInfo()
                .GetAllProperties()
                .Where(p => p.CanRead)
                .ToArray();

            var linkProperties = allProperties
                .Where(p => p.CanWrite && typeof(Link).IsAssignableFrom(p.PropertyType));

            foreach (var linkProperty in linkProperties)
            {
                var rewritten = rewriter.Rewrite(linkProperty.GetValue(model) as RouteLink);
                if (rewritten == null) continue;

                linkProperty.SetValue(model, rewritten);

                // Special handling of the hidden Self property
                if (linkProperty.Name == nameof(Resource.Self))
                {
                    allProperties.SingleOrDefault(p => p.Name == nameof(Resource.Href))
                        ?.SetValue(model, rewritten.Href);
                    allProperties.SingleOrDefault(p => p.Name == nameof(Resource.Method))
                        ?.SetValue(model, rewritten.Method);
                    allProperties.SingleOrDefault(p => p.Name == nameof(Resource.Relations))
                        ?.SetValue(model, rewritten.Relations);
                }
            }

            var arrayProperties = allProperties.Where(p => p.PropertyType.IsArray);
            RewriteLinksInArrays(arrayProperties, model, rewriter);

            var objectProperties = allProperties.Except(linkProperties).Except(arrayProperties);
            RewriteLinksInNestedObjects(objectProperties, model, rewriter);
        }

        private static void RewriteLinksInNestedObjects(
            IEnumerable<PropertyInfo> objectProperties,
            object obj,
            LinkRewriter rewriter)
        {
            foreach (var objectProperty in objectProperties)
            {
                var shouldSkip = objectProperty.PropertyType == typeof(string);
                if (shouldSkip) continue;

                var typeInfo = objectProperty.PropertyType.GetTypeInfo();
                if (typeInfo.IsClass)
                {
                    RewriteAllLinks(objectProperty.GetValue(obj), rewriter);
                }
            }
        }

        private static void RewriteLinksInArrays(
            IEnumerable<PropertyInfo> arrayProperties,
            object obj,
            LinkRewriter rewriter)
        {
            foreach (var arrayProperty in arrayProperties)
            {
                var array = arrayProperty.GetValue(obj) as Array ?? new Array[0];

                foreach (var element in array)
                {
                    RewriteAllLinks(element, rewriter);
                }
            }
        }
    }
}
