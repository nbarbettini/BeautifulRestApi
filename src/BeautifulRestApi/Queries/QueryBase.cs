using System.Threading;
using System.Threading.Tasks;

namespace BeautifulRestApi.Queries
{
    public abstract class QueryBase<T> : IQuery<T>
    {
        public BeautifulContext Context { get; private set; }

        public QueryExecutor Executor { get; private set; }

        public void Initialize(BeautifulContext context, QueryExecutor executor)
        {
            Context = context;
            Executor = executor;
        }

        public abstract Task<T> ExecuteAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
