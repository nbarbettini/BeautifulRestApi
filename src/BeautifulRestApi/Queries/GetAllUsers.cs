using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.DbModels;

namespace BeautifulRestApi.Queries
{
    public class GetAllUsers : QueryBase<IQueryable<DbUser>>
    {
        public override Task<IQueryable<DbUser>> ExecuteAsync(CancellationToken cancellationToken = new CancellationToken())
            => Task.FromResult(Context.Users as IQueryable<DbUser>);
    }
}
