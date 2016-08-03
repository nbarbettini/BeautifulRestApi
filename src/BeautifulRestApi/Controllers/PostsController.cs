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

        [HttpGet]
        public async Task<IActionResult> Get(PagedCollectionParameters parameters)
        {
            var getAllQuery = new GetAllPostsQuery(_context, _defaultPagingOptions, Endpoint);
            var results = await getAllQuery.Execute(parameters);

            // Attach form definitions for discoverability
            results.Forms = new[] { Form.FromModel<PostCreateModel>(Endpoint, "POST", "create-form") };

            return new ObjectResult(results);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var query = new GetPostQuery(_context);
            var post = await query.Execute(id);

            return post == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(post);
        }

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
