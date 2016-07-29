using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public class GetAllPeopleQuery : QueryBase
    {
        private readonly string _endpoint;
        private readonly PagedCollectionParameters _defaultPagingParameters;

        public GetAllPeopleQuery(BeautifulContext context, string endpoint, PagedCollectionParameters defaultPagingParameters)
            : base(context)
        {
            _endpoint = endpoint;
            _defaultPagingParameters = defaultPagingParameters;
        }

        public Task<PagedCollection<PersonResponse>> Execute(PagedCollectionParameters parameters)
        {
            var collectionFactory = new PagedCollectionFactory<PersonResponse>(_endpoint);

            return collectionFactory.CreateFrom(
                Context.People,
                person => new PersonResponse(new ResourceLink(_endpoint, person.Id), person.FirstName, person.LastName, person.BirthDate),
                parameters.Offset ?? _defaultPagingParameters.Offset.Value,
                parameters.Limit ?? _defaultPagingParameters.Limit.Value);
        }
    }
}
