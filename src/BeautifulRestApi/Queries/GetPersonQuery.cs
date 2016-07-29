using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Queries
{
    public class GetPersonQuery
    {
        private readonly BeautifulContext _context;

        public GetPersonQuery(BeautifulContext context)
        {
            _context = context;
        }

        public async Task<Person> Execute(string id)
        {
            var p = await _context.People.SingleOrDefaultAsync(x => x.Id == id);

            return p == null
                ? null
                : p.Adapt<Person>();
        }
    }
}
