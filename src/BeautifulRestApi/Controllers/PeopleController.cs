using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using BeautifulRestApi.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BeautifulRestApi.Controllers
{
    public class PeopleController
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
        [Route(Endpoint)]
        public async Task<IActionResult> Get(PagedCollectionParameters parameters)
        {
            var getAllQuery = new GetAllPeopleQuery(_context, Endpoint, _defaultPagingOptions);

            var results = await getAllQuery.Execute(parameters);

            // Attach form definitions for discoverability
            results.Forms = new[] {GetPeopleCollectionCreateForm()};

            return new ObjectResult(results);
        }

        [HttpGet]
        [Route(Endpoint + "/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var getQuery = new GetPersonQuery(_context, Endpoint);

            var person = await getQuery.Execute(id);

            return person == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(person);
        }

        private static Form GetPeopleCollectionCreateForm() =>
            new Form("people", HttpMethod.Post.Method, "create-form", new[]
            {
                new FormField { Name = "firstName", Type = "string", Required = true },
                new FormField { Name = "lastName", Type = "string", Required = true},
                new FormField { Name = "birthDate", Type = "datetime", Required = false } 
            });
    }
}

