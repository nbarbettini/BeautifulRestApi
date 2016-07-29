using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Queries
{
    public class GetPersonQuery : QueryBase
    {
        private readonly string _endpoint;

        public GetPersonQuery(BeautifulContext context, string endpoint)
            : base(context)
        {
            _endpoint = endpoint;
        }

        public async Task<Person> Execute(string id)
        {
            var p = await Context.People.SingleOrDefaultAsync(x => x.Id == id);

            return p == null
                ? null
                : p.Adapt<Person>();
        }
    }
}
