using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeautifulRestApi.Filters
{
    public abstract class AbstractResultEnricher<T> : IResultEnricher
        where T : class
    {
        public bool CanEnrich(object result)
            => CanEnrich(result.GetType());

        private static bool CanEnrich(Type type)
        {
            if (type == typeof(T))
            {
                return true;
            }

            return type != null && CanEnrich(type.GetTypeInfo().BaseType);
        }

        public void Enrich(ResultExecutingContext context, object result)
        {
            var concrete = result as T;
            if (concrete == null)
            {
                return;
            }

            OnEnriching(context, concrete);
        }

        protected abstract void OnEnriching(ResultExecutingContext context, T result);
    }
}
