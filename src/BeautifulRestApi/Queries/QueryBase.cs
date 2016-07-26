using System;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public abstract class QueryBase<T>
        where T : Resource
    {
        protected QueryBase(BeautifulContext context)
        {
            Context = context;
        }

        protected BeautifulContext Context { get; }

        protected string ConstructResourceHref(string baseHref, string collectionName, string id)
            => string.Join("/", baseHref.TrimEnd('/'), collectionName, id);
    }
}
