using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Queries
{
    public class GetOrderQuery
    {
        private readonly BeautifulContext _context;

        public GetOrderQuery(BeautifulContext context)
        {
            _context = context;
        }

        public async Task<Order> Execute(string id)
        {
            var p = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);

            return p == null
                ? null
                : p.Adapt<Order>();
        }
    }
}
