using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
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

        public async Task<PersonResponse> Execute(string id)
        {
            var p = await Context.People.SingleOrDefaultAsync(x => x.Id == id);

            return p == null
                ? null
                : new PersonResponse(new ResourceLink(_endpoint, p.Id), p.FirstName, p.LastName, p.BirthDate);
        }
    }
}
