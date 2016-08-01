using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Controllers;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class GetOrdersByPersonQuery
    {
        private readonly Dal.BeautifulContext _context;
        private readonly PagedCollectionParameters _defaultPagingParameters;
        private readonly string _endpoint;
        private readonly string _personId;

        public GetOrdersByPersonQuery(Dal.BeautifulContext context, PagedCollectionParameters defaultPagingParameters, string endpoint, string id)
        {
            _context = context;
            _defaultPagingParameters = defaultPagingParameters;
            _endpoint = endpoint;
            _personId = id;

        }

        public Task<PagedCollection<Order>> Execute(string personId, PagedCollectionParameters parameters)
        {
            var meta = PlaceholderLink.ToCollection(_endpoint, values: new { id = _personId, link = OrdersController.Endpoint });
            var collectionFactory = new PagedCollectionFactory<Order>(meta);

            var query = _context.Orders
                .Where(o => o.PersonId == personId)
                .ProjectToType<Order>();

            return collectionFactory.CreateFrom(query,
                parameters.Offset ?? _defaultPagingParameters.Offset.Value,
                parameters.Limit ?? _defaultPagingParameters.Limit.Value);
        }
    }
}
