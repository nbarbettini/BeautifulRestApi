using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class GetAllUsersQuery
    {
        private readonly BeautifulContext _context;
        private readonly string _endpoint;
        private readonly PagedCollectionParameters _defaultPagingParameters;

        public GetAllUsersQuery(BeautifulContext context, string endpoint, PagedCollectionParameters defaultPagingParameters)
        {
            _context = context;
            _endpoint = endpoint;
            _defaultPagingParameters = defaultPagingParameters;
        }

        public Task<PagedCollection<User>> Execute(PagedCollectionParameters parameters)
        {
            var collectionFactory = new PagedCollectionFactory<User>(PlaceholderLink.ToCollection(_endpoint));

            return collectionFactory.CreateFrom(
                _context.Users.ProjectToType<User>(),
                parameters.Offset ?? _defaultPagingParameters.Offset.Value,
                parameters.Limit ?? _defaultPagingParameters.Limit.Value);
        }
    }
}
