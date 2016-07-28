//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;
//using BeautifulRestApi.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc.Routing;

//namespace BeautifulRestApi
//{
//    public class LinkFormattingFilter : IAsyncResultFilter
//    {
//        private readonly IUrlHelperFactory _urlHelperFactory;
//        private readonly ILinkFormatter[] _linkFormatters;

//        public LinkFormattingFilter(IUrlHelperFactory urlHelperFactory, IEnumerable<ILinkFormatter> linkFormatters)
//        {
//            _urlHelperFactory = urlHelperFactory;
//            _linkFormatters = linkFormatters.ToArray();
//        }

//        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
//        {
//            var asObjectResult = context.Result as ObjectResult;
//            if (asObjectResult == null)
//            {
//                return;
//            }

//            var urlHelper = _urlHelperFactory.GetUrlHelper(context);
//            EnrichLinks(asObjectResult.Value, urlHelper);

//            await next();
//        }
//    }
//}
