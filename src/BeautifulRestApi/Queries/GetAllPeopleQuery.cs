using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public class GetAllPeopleQuery : QueryBase
    {
        private const string Endpoint = "people";

        public GetAllPeopleQuery(BeautifulContext context)
            : base(context)
        {
        }

        public Task<PagedCollection<PersonResponse>> Execute(int offset = 0, int limit = 25)
        {
            var collectionFactory = new PagedCollectionFactory<PersonResponse>(Endpoint);

            return collectionFactory.CreateFrom(
                Context.People,
                person => new PersonResponse(new ResourceLink(Endpoint, person.Id), person.FirstName, person.LastName, person.BirthDate),
                offset,
                limit);
        }
    }
}
