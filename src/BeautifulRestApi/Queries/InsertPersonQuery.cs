using System;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public class InsertPersonQuery : QueryBase
    {
        private readonly string _endpoint;

        public InsertPersonQuery(BeautifulContext context, string endpoint)
            : base(context)
        {
            _endpoint = endpoint;
        }

        public async Task<Person> Execute(PersonCreateModel model)
        {
            var entry = Context.People.Add(new Dal.DbModels.Person
            {
                Id = Guid.NewGuid().ToString().Replace("-", string.Empty),
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate
            });

            await Context.SaveChangesAsync();

            return new Person
            {
                Meta = new ResourceLink(_endpoint, entry.Entity.Id),
                FirstName = entry.Entity.FirstName,
                LastName = entry.Entity.LastName,
                BirthDate = entry.Entity.BirthDate
            };
        }
    }
}
