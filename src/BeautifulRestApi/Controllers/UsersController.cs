using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using BeautifulRestApi.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BeautifulRestApi.Controllers
{
    [Route(Endpoint)]
    public class UsersController : Controller
    {
        public const string Endpoint = "users";

        private readonly BeautifulContext _context;
        private readonly PagedCollectionParameters _defaultPagingOptions;

        public UsersController(BeautifulContext context, IOptions<PagedCollectionParameters> defaultPagingOptions)
        {
            _context = context;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Get(PagedCollectionParameters parameters)
        {
            var executor = new QueryExecutor(_context);
            var results = await executor.ExecuteAsync(
                new GetAllUsers(),
                new ProjectToPagedCollection<DbModels.DbUser, User>()
                {
                    Meta = PlaceholderLink.ToCollection(Endpoint),
                    DefaultPagingParameters = _defaultPagingOptions,
                    PagingParameters = parameters
                });

            // Attach form definitions for discoverability
            results.Forms = new[] { Form.FromModel<UserCreateModel>(Endpoint, "POST", "create-form") };

            return new ObjectResult(results);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var getUserQuery = new GetUser {Id = id};
            var executor = new QueryExecutor(_context);

            var user = await executor.ExecuteAsync(getUserQuery, new ProjectUser());

            return user == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(user);
        }

        [HttpGet]
        [Route("{id}/posts")]
        public async Task<IActionResult> GetPosts(string id, PagedCollectionParameters parameters)
        {
            var executor = new QueryExecutor(_context);

            var posts = await executor.ExecuteAsync(
                new GetPostsByUser {UserId = id},
                new ProjectToPagedCollection<DbModels.DbPost, Post>()
                {
                    Meta = PlaceholderLink.ToCollection(Endpoint, values: new { id, link = PostsController.Endpoint }),
                    DefaultPagingParameters = _defaultPagingOptions,
                    PagingParameters = parameters
                });

            return new ObjectResult(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = ModelState.Values.First().Errors.First().ErrorMessage
                });
            }

            var createQuery = new CreateUserQuery(_context);
            var user = await createQuery.Execute(model);

            return new CreatedAtRouteResult("default", new { controller = Endpoint, id = user.Item1}, user.Item2);
        }
    }
}

