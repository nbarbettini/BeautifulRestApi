using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using BeautifulRestApi.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BeautifulRestApi.Controllers
{
    [Route(Endpoint)]
    public class PostsController : Controller
    {
        public const string Endpoint = "posts";

        private readonly BeautifulContext _context;
        private readonly PagedCollectionParameters _defaultPagingOptions;

        public PostsController(BeautifulContext context, IOptions<PagedCollectionParameters> defaultPagingOptions)
        {
            _context = context;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        public async Task<IActionResult> Get(PagedCollectionParameters parameters)
        {
            var executor = new QueryExecutor(_context);
            var results = await executor.ExecuteAsync(
                new GetAllPosts(),
                new ProjectToPagedCollection<DbModels.DbPost, Post>()
                {
                    Meta = PlaceholderLink.ToCollection(Endpoint),
                    DefaultPagingParameters = _defaultPagingOptions,
                    PagingParameters = parameters
                },
                new AddForms<PagedCollection<Post>>
                {
                    Forms = new[]
                    {
                        Form.FromModel<PostCreateModel>(Endpoint, "POST", "create-form")
                    }

                });

            return new ObjectResult(results);
        }

        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var executor = new QueryExecutor(_context);

            var post = await executor.ExecuteAsync(
                new GetPost {PostId = id},
                new ProjectPost());

            return post == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(post);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = ModelState.Values.First().Errors.First().ErrorMessage
                });
            }

            var createQuery = new CreatePostQuery(_context);
            var post = await createQuery.Execute(model);

            return new CreatedAtRouteResult("default", new { controller = Endpoint, id = post.Item1 }, post.Item2);
        }
    }
}
