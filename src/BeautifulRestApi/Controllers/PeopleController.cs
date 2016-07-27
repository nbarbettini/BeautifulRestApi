using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using BeautifulRestApi.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BeautifulRestApi.Controllers
{
    public class PeopleController : ControllerBase
    {
        public PeopleController(BeautifulContext context)
            : base(context)
        {
        }

        [HttpGet]
        [Route("people")]
        public async Task<IActionResult> Get()
        {
            var getAllQuery = new GetAllPeopleQuery(
                DataContext,
                UrlHelper.Construct(RootHref, "people"));

            dynamic results = await getAllQuery.Execute();

            // Attach form definitions for discoverability
            results.Forms = new[] {GetPeopleCollectionCreateForm(RootHref), GetPeopleCollectionSearchForm(RootHref)};

            return new ObjectResult(results);
        }

        [HttpGet]
        [Route("people/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var getQuery = new GetPersonQuery(
                DataContext,
                UrlHelper.Construct(RootHref, "people"));

            var person = await getQuery.Execute(id);

            return person == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(person);
        }

        private static Form GetPeopleCollectionCreateForm(string baseHref) =>
            new Form(UrlHelper.Construct(baseHref, "people"), HttpMethod.Post.Method, new[]
            {
                new FormField() { Name = "firstName", Type = "string", Required = true },
                new FormField() { Name = "lastName", Type = "string", Required = true},
                new FormField() { Name = "birthDate", Type = "datetime", Required = false } 
            });

        private static Form GetPeopleCollectionSearchForm(string baseHref) => null;
    }
}

