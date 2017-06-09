using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly ApiDbContext _context;

        public UsersController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name = nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int id)
        {
            var dbUser = await _context.Users
                .Where(dbu => dbu.Id == id)
                .SingleOrDefaultAsync();

            if (dbUser == null)
            {
                return NotFound();
            }

            var response = new User()
            {
                Href = Url.Link(nameof(GetUserById), new { id = dbUser.Id }),
                Method = "GET",
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName
            };

            return Ok(response);
        }

        [HttpGet(Name = nameof(GetAllUsers))]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = (await _context.Users.ToArrayAsync())
                .Select(dbUser => new User
                {
                    Href = Url.Link(nameof(GetUserById), new { id = dbUser.Id }),
                    Method = "GET",
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName
                })
                .ToArray();

            var response = new Collection<User>
            {
                Href = Url.Link(nameof(GetAllUsers), null),
                Relations = new[] { "collection" },
                Value = users
            };

            return Ok(response);
        }

    }
}