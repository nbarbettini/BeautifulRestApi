using System;
using System.Threading.Tasks;
using BeautifulRestApi.DbModels;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class CreateUserQuery
    {
        private readonly BeautifulContext _context;

        public CreateUserQuery(BeautifulContext context)
        {
            _context = context;
        }

        public async Task<Tuple<string, User>> Execute(UserCreateModel model)
        {
            var entry = _context.Users.Add(new DbUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate
            });

            await _context.SaveChangesAsync();

            return new Tuple<string, User>(
                entry.Entity.Id,
                entry.Entity.Adapt<User>());
        }
    }
}
