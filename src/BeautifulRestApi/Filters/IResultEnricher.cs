using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeautifulRestApi.Filters
{
    public interface IResultEnricher
    {
        bool CanEnrich(object result);

        void Enrich(ResultExecutingContext context, object result, Action<ResultExecutingContext, object> enrichChildAction);
    }
}
