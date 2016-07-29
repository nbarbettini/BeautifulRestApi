using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeautifulRestApi.Filters
{
    public class ResultEnrichingFilter : IAsyncResultFilter
    {
        private readonly IResultEnricher[] _enrichers;

        public ResultEnrichingFilter(IEnumerable<IResultEnricher> enrichers)
        {
            _enrichers = enrichers.ToArray();
        }

        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var asObjectResult = context.Result as ObjectResult;
            if (asObjectResult != null)
            {
                EnrichResult(context, asObjectResult.Value);
            }

            return next();
        }

        private void EnrichResult(ResultExecutingContext context, object result)
        {
            foreach (var enricher in _enrichers)
            {
                if (enricher.CanEnrich(result))
                {
                    enricher.Enrich(context, result, EnrichResult);
                }
            }
        }
    }
}
