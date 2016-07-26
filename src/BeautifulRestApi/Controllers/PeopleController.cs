using BeautifulRestApi.Dal;
using BeautifulRestApi.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BeautifulRestApi.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly BeautifulContext _context;

        public PeopleController(BeautifulContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var getAllQuery = new PeopleGetAllQuery(_context);

            return new ObjectResult(getAllQuery.Execute());
        }
    }
}

