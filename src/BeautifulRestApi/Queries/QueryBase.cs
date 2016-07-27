using System;
using System.Linq;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public abstract class QueryBase
    {
        protected QueryBase(BeautifulContext context)
        {
            Context = context;
        }

        protected BeautifulContext Context { get; }
    }
}
