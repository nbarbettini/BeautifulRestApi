using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using BeautifulRestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeautifulRestApi.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_peopleService.GetAll());
        }

        //[HttpGet("all")]
        //public IActionResult GetAll()
        //{
        //    return new ObjectResult(new Collection<PersonResponse>("https://example.io/users", _people));
        //}
    }
}

