using System.Linq;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public class PeopleGetAllQuery : QueryBase<PersonResponse>
    {
        public PeopleGetAllQuery(BeautifulContext context)
            : base(context)
        {
        }

        public override PersonResponse[] Execute()
        {
            return Context.People
                .Select(src => new PersonResponse(src.Href, src.FirstName, src.LastName, src.BirthDate))
                .ToArray();
        }
    }
}
