using BeautifulRestApi.Dal;

namespace BeautifulRestApi.Queries
{
    public abstract class QueryBase<T>
        where T : class
    {
        protected QueryBase(BeautifulContext context)
        {
            Context = context;
        }

        protected BeautifulContext Context { get; }

        public abstract T[] Execute();
    }
}
