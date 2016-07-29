using System;
using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeautifulRestApi.Filters
{
    public class CollectionEnricher : AbstractResultEnricher<ICollection>
    {
        protected override void OnEnriching(ResultExecutingContext context, ICollection result, Action<ResultExecutingContext, object> enrichAction)
        {
            var itemsEnumerator = result.GetEnumerator();
            while (itemsEnumerator.MoveNext())
            {
                enrichAction(context, itemsEnumerator.Current);
            }
        }
    }
}
