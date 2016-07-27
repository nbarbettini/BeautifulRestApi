using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public class GetAllPeopleQuery : QueryBase
    {
        private readonly string _baseHref;

        public GetAllPeopleQuery(BeautifulContext context, string baseHref)
            : base(context)
        {
            _baseHref = baseHref;
        }

        public Task<PagedCollectionResponse<PersonResponse>> Execute(int offset = 0, int limit = 25)
        {
            var collectionFactory = new PagedCollectionFactory<PersonResponse>(_baseHref);

            return collectionFactory.CreateFrom(
                Context.People,
                person => new PersonResponse(UrlHelper.Construct(_baseHref, person.Id), person.FirstName, person.LastName, person.BirthDate),
                offset,
                limit);
        }
    }
}
