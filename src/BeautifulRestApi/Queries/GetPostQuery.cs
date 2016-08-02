using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Queries
{
    public class GetPostQuery
    {
        private readonly BeautifulContext _context;

        public GetPostQuery(BeautifulContext context)
        {
            _context = context;
        }

        public async Task<Post> Execute(string id)
        {
            var p = await _context.Posts.SingleOrDefaultAsync(x => x.Id == id);

            return p == null
                ? null
                : p.Adapt<Post>();
        }
    }
}
