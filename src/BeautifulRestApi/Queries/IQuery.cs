using System.Threading;
using System.Threading.Tasks;

namespace BeautifulRestApi.Queries
{
    public interface IQuery<T>
    {
        void Initialize(BeautifulContext context);

        Task<T> ExecuteAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
