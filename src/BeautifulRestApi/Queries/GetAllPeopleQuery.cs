using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public class GetAllPeopleQuery : QueryBase
    {
        private readonly string _endpoint;

        public GetAllPeopleQuery(BeautifulContext context, string endpoint)
            : base(context)
        {
            _endpoint = endpoint;
        }

        public Task<PagedCollection<PersonResponse>> Execute(int offset = 0, int limit = 25)
        {
            var collectionFactory = new PagedCollectionFactory<PersonResponse>(_endpoint);

            return collectionFactory.CreateFrom(
                Context.People,
                person => new PersonResponse(new ResourceLink(_endpoint, person.Id), person.FirstName, person.LastName, person.BirthDate),
                offset,
                limit);
        }
    }
}
