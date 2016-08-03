using System;
using System.Threading;
using System.Threading.Tasks;

namespace BeautifulRestApi.Queries
{
    public class QueryExecutor
    {
        private readonly BeautifulContext _context;

        public QueryExecutor(BeautifulContext context)
        {
            _context = context;
        }

        public Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            query.Initialize(_context, this);

            return query.ExecuteAsync(cancellationToken);
        }

        public async Task<TTransformed> ExecuteAsync<TResult, TTransformed>(
            IQuery<TResult> query,
            ITransformation<TResult, TTransformed> transformation,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryResults = await ExecuteAsync(query, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            return await transformation.ExecuteAsync(queryResults, cancellationToken);
        }

        public async Task<TTransformed2> ExecuteAsync<TResult, TTransformed1, TTransformed2>(
            IQuery<TResult> query,
            ITransformation<TResult, TTransformed1> transformation1,
            ITransformation<TTransformed1, TTransformed2> transformation2,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryResults = await ExecuteAsync(query, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            return await transformation2.ExecuteAsync(
                await transformation1.ExecuteAsync(queryResults, cancellationToken),
                cancellationToken);
        }
    }
}
