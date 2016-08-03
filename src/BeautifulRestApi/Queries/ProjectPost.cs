using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.DbModels;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class ProjectPost : ITransformation<DbPost, Post>
    {
        public Task<Post> ExecuteAsync(DbPost input, CancellationToken cancellationToken = new CancellationToken())
            => Task.FromResult(input?.Adapt<Post>());
    }
}
