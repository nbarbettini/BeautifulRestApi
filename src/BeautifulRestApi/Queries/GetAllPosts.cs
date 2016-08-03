using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.DbModels;

namespace BeautifulRestApi.Queries
{
    public class GetAllPosts : QueryBase<IQueryable<DbPost>>
    {
        public override Task<IQueryable<DbPost>> ExecuteAsync(CancellationToken cancellationToken = new CancellationToken())
            => Task.FromResult(Context.Posts as IQueryable<DbPost>);
    }
}
