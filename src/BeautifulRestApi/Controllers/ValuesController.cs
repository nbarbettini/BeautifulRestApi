using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeautifulRestApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly Person[] _people = {
            new Person("https://example.io/people/1")
            {
                FirstName = "Bob",
                LastName = "Smith",
                BirthDate = new DateTimeOffset(1985, 06, 15, 12, 00, 00, TimeSpan.Zero)
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_people.First());
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return new ObjectResult(new Collection<Person>("https://example.io/users", _people));
        }
    }
}

