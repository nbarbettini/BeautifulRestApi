using System;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class InsertPersonQuery
    {
        private readonly BeautifulContext _context;

        public InsertPersonQuery(BeautifulContext context)
        {
            _context = context;
        }

        public async Task<Tuple<string, Person>> Execute(PersonCreateModel model)
        {
            var entry = _context.People.Add(new Dal.DbModels.Person
            {
                Id = Dal.IdGenerator.GetId(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate
            });

            await _context.SaveChangesAsync();

            return new Tuple<string, Person>(
                entry.Entity.Id,
                entry.Entity.Adapt<Person>());
        }
    }
}
