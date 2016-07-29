using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using BeautifulRestApi.Queries;
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
            var getQuery = new GetPersonQuery(_context, Endpoint);
            var person = await getQuery.Execute(id);

            return person == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(person);
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

            var createQuery = new InsertPersonQuery(_context, Endpoint);
            var person = await createQuery.Execute(model);

            return new CreatedAtRouteResult("default", new { controller = Endpoint, id = (person.Meta as ResourceLink).Id}, person);
        }
    }
}

