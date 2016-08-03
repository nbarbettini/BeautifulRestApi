using System.Threading;
using System.Threading.Tasks;

namespace BeautifulRestApi.Queries
{
    public interface ITransformation<T, TResult>
    {
        Task<TResult> ExecuteAsync(T input, CancellationToken cancellationToken = default(CancellationToken));
    }
}
