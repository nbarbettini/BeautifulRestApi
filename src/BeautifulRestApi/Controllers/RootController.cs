using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeautifulRestApi.Controllers
{
    [Route("/")]
    public class RootController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(new RootModel());
        }
    }
}
