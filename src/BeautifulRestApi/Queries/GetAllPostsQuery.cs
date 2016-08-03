using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class GetAllPostsQuery
    {
        private readonly BeautifulContext _context;
        private readonly PagedCollectionParameters _defaultPagingParameters;
        private readonly string _endpoint;

        public GetAllPostsQuery(BeautifulContext context, PagedCollectionParameters defaultPagingParameters, string endpoint)
        {
            _context = context;
            _defaultPagingParameters = defaultPagingParameters;
            _endpoint = endpoint;
        }

        public Task<PagedCollection<Post>> Execute(PagedCollectionParameters parameters)
        {
            var collectionFactory = new PagedCollectionFactory<Post>(PlaceholderLink.ToCollection(_endpoint));

            return collectionFactory.CreateFrom(
                _context.Posts.ProjectToType<Post>(),
                parameters.Offset ?? _defaultPagingParameters.Offset.Value,
                parameters.Limit ?? _defaultPagingParameters.Limit.Value);
        }
    }
}
