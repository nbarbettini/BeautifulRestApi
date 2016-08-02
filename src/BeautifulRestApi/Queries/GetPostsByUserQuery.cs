using System;
using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Controllers;
using BeautifulRestApi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Queries
{
    public class GetPostsByUserQuery
    {
        private readonly Dal.BeautifulContext _context;
        private readonly PagedCollectionParameters _defaultPagingParameters;
        private readonly string _endpoint;

        public GetPostsByUserQuery(Dal.BeautifulContext context, PagedCollectionParameters defaultPagingParameters, string endpoint)
        {
            _context = context;
            _defaultPagingParameters = defaultPagingParameters;
            _endpoint = endpoint;

        }

        public async Task<PagedCollection<Post>> Execute(string userId, PagedCollectionParameters parameters)
        {
            var meta = PlaceholderLink.ToCollection(_endpoint, values: new { id = userId, link = PostsController.Endpoint });
            var collectionFactory = new PagedCollectionFactory<Post>(meta);

            var user = await _context.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            // todo
            throw new NotImplementedException();

            //var query = _context.Orders
            //    .Where(o => o.PersonId == userId)
            //    .ProjectToType<Post>();

            //return await collectionFactory.CreateFrom(
            //    person.,
            //    parameters.Offset ?? _defaultPagingParameters.Offset.Value,
            //    parameters.Limit ?? _defaultPagingParameters.Limit.Value);
        }
    }
}
