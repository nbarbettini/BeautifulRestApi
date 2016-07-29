using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using Mapster;

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
            //_adapter = adapter;
        }

        public Task<PagedCollection<Person>> Execute(PagedCollectionParameters parameters)
        {
            var collectionFactory = new PagedCollectionFactory<Person>(_endpoint);

            return collectionFactory.CreateFrom(
                Context.People.ProjectToType<Person>(),
                parameters.Offset ?? _defaultPagingParameters.Offset.Value,
                parameters.Limit ?? _defaultPagingParameters.Limit.Value);
        }
    }
}
