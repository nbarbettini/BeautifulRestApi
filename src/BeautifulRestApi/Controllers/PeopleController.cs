using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using BeautifulRestApi.Queries;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BeautifulRestApi.Controllers
{
    [Route(Endpoint)]
    public class PeopleController : Controller
    {
        public const string Endpoint = "people";

        private readonly BeautifulContext _context;
        private readonly PagedCollectionParameters _defaultPagingOptions;

        public PeopleController(BeautifulContext context, IOptions<PagedCollectionParameters> defaultPagingOptions)
        {
            _context = context;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Get(PagedCollectionParameters parameters)
        {
            var getAllQuery = new GetAllPeopleQuery(_context, Endpoint, _defaultPagingOptions);
            var results = await getAllQuery.Execute(parameters);

            // Attach form definitions for discoverability
            results.Forms = new[] {Form.FromModel<PersonCreateModel>(Endpoint, "POST", "create-form")};

            return new ObjectResult(results);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var getQuery = new GetPersonQuery(_context);
            var person = await getQuery.Execute(id);

            return person == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(person);
        }

        [HttpGet]
        [Route("{id}/orders")]
        public async Task<IActionResult> GetOrders(string id, PagedCollectionParameters parameters)
        {
            var getOrdersByPersonQuery = new GetOrdersByPersonQuery(_context, _defaultPagingOptions, Endpoint, id);

            return new ObjectResult(await getOrdersByPersonQuery.Execute(id, parameters));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = ModelState.Values.First().Errors.First().ErrorMessage
                });
            }

            var createQuery = new InsertPersonQuery(_context);
            var person = await createQuery.Execute(model);

            return new CreatedAtRouteResult("default", new { controller = Endpoint, id = person.Item1}, person.Item2);
        }
    }
}

