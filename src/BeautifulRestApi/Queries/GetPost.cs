using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Queries
{
    public class GetPost : QueryBase<DbPost>
    {
        public string PostId { get; set; }

        public override Task<DbPost> ExecuteAsync(CancellationToken cancellationToken = new CancellationToken())
            => Context.Posts.SingleOrDefaultAsync(x => x.Id == PostId, cancellationToken);
    }
}
