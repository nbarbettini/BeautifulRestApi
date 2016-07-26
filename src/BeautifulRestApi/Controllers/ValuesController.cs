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
        // GET api/values
        [HttpGet]
        public Person Get()
        {
            return new Person
            {
                Meta = new Metadata {Href = "https://example.io/people/1"},
                FirstName = "Bob",
                LastName = "Smith",
                BirthDate = new DateTimeOffset(1985, 06, 15, 12, 00, 00, TimeSpan.Zero)
            };
        }
    }
}

