using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Queries
{
    public class GetPostQuery
    {
        private readonly BeautifulContext _context;
        private readonly TypeAdapterConfig _typeAdapterConfig;

        public GetPostQuery(BeautifulContext context, TypeAdapterConfig typeAdapterConfig)
        {
            _context = context;
            _typeAdapterConfig = typeAdapterConfig;
        }

        public async Task<Post> Execute(string id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.Id == id);

            return post?.Adapt<Post>(_typeAdapterConfig);
        }
    }
}
