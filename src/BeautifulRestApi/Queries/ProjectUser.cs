using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.DbModels;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class ProjectUser : ITransformation<DbUser, User>
    {
        public Task<User> ExecuteAsync(DbUser input, CancellationToken cancellationToken = new CancellationToken())
            => Task.FromResult(input?.Adapt<User>());
    }
}
