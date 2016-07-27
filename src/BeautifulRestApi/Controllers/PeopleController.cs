using System.Threading.Tasks;
using BeautifulRestApi.Dal;
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

            return new ObjectResult(await getAllQuery.Execute());
        }

        [HttpGet]
        [Route("people/{id}")]
        public IActionResult Get(string id)
        {
            var getQuery = new GetPersonQuery(
                DataContext,
                UrlHelper.Construct(RootHref, "people"));

            var person = getQuery.Execute(id);

            return person == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(person);
        }
    }
}

