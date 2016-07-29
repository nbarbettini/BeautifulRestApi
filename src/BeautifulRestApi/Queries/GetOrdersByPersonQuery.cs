using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class GetOrdersByPersonQuery
    {
        private readonly Dal.BeautifulContext _context;
        private readonly string _endpoint;
        private readonly PagedCollectionParameters _defaultPagingParameters;

        public GetOrdersByPersonQuery(Dal.BeautifulContext context, string endpoint, PagedCollectionParameters defaultPagingParameters)
        {
            _context = context;
            _endpoint = endpoint;
            _defaultPagingParameters = defaultPagingParameters;
        }

        public Task<PagedCollection<Order>> Execute(string personId, PagedCollectionParameters parameters)
        {
            var collectionFactory = new PagedCollectionFactory<Order>(_endpoint);

            var query = _context.Orders
                .Where(o => o.PersonId == personId)
                .ProjectToType<Order>();

            return collectionFactory.CreateFrom(query,
                parameters.Offset ?? _defaultPagingParameters.Offset.Value,
                parameters.Limit ?? _defaultPagingParameters.Limit.Value);
        }
    }
}
