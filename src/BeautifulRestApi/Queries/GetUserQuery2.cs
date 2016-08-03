using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Queries
{
    public class GetUserQuery2 : QueryBase<DbUser>
    {
        public string Id { get; set; }

        public override Task<DbUser> ExecuteAsync(CancellationToken cancellationToken = new CancellationToken())
            => Context.Users.SingleOrDefaultAsync(x => x.Id == Id, cancellationToken);
    }
}
