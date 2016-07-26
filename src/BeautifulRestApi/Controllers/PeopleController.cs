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
        public IActionResult Get()
        {
            var getAllQuery = new PeopleGetAllQuery(DataContext);

            return new ObjectResult(getAllQuery.Execute(BaseHref));
        }

        [HttpGet]
        [Route("people/{id}")]
        public IActionResult Get(string id)
        {
            var getQuery = new PeopleGetQuery(DataContext, BaseHref);

            var person = getQuery.Execute(id);

            return person == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(person);
        }
    }
}

