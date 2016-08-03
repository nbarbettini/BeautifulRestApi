using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.DbModels;

namespace BeautifulRestApi.Queries
{
    public class GetPostsByUser : QueryBase<IQueryable<DbPost>>
    {
        public string UserId { get; set; }

        public override async Task<IQueryable<DbPost>> ExecuteAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var getUserQuery = new GetUser { Id = UserId };
            var user = await Executor.ExecuteAsync(getUserQuery, cancellationToken);

            return user == null 
                ? null
                : Context.Posts.Where(o => o.UserId == UserId);
        }
    }
}
