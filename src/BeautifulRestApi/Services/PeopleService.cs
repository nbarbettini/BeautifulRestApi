using System.Collections.Generic;
using System.Linq;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly BeautifulContext _context;

        public PeopleService(BeautifulContext context)
        {
            _context = context;
        }

        public IEnumerable<PersonResponse> GetAll()
        {
            return _context.People
                .Select(src => new PersonResponse(src.Href, src.FirstName, src.LastName, src.BirthDate))
                .ToArray();
        }
    }
}
