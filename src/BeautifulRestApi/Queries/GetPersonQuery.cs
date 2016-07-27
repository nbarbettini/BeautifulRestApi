using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Queries
{
    public class GetPersonQuery : QueryBase
    {
        private readonly string _baseHref;

        public GetPersonQuery(BeautifulContext context, string baseHref)
            : base(context)
        {
            _baseHref = baseHref;
        }

        public async Task<PersonResponse> Execute(string id)
        {
            var p = await Context.People.SingleOrDefaultAsync(x => x.Id == id);

            return p == null
                ? null
                : new PersonResponse(UrlHelper.Construct(_baseHref, p.Id), p.FirstName, p.LastName, p.BirthDate);
        }
    }
}
