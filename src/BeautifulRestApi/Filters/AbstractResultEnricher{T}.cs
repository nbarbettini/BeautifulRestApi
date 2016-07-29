using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeautifulRestApi.Filters
{
    public abstract class AbstractResultEnricher<T> : IResultEnricher
        where T : class
    {
        public bool CanEnrich(object result)
            => result != null && CanEnrich(result.GetType());

        public void Enrich(ResultExecutingContext context, object result, Action<ResultExecutingContext, object> enrichAction)
        {
            var concrete = result as T;
            if (concrete == null)
            {
                return;
            }

            OnEnriching(context, concrete, enrichAction);
        }

        private static bool CanEnrich(Type type)
        {
            if (typeof(T).IsAssignableFrom(type))
            {
                return true;
            }

            return type != null && CanEnrich(type.GetTypeInfo().BaseType);
        }

        protected abstract void OnEnriching(ResultExecutingContext context, T result, Action<ResultExecutingContext, object> enrichAction);
    }
}
