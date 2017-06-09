using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeautifulRestApi.Controllers
{
    [Route("/")]
    public class RootController : Controller
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                users = new
                {
                    href = Url.Link(nameof(UsersController.GetAllUsers), null),
                    rel = new[] { "collection" }
                }
            };

            return Ok(response);
        }
    }

}
