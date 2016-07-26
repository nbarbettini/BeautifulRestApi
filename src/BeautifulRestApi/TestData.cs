using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;

namespace BeautifulRestApi
{
    public static class TestData
    {
        private static readonly Dal.DbModels.Person[] PeopleData = {
            new Dal.DbModels.Person
            {
                Href = "https://example.io/people/1",
                FirstName = "Bob",
                LastName = "Smith",
                BirthDate = new DateTimeOffset(1985, 6, 12, 15, 00, 00, TimeSpan.Zero)
            },
            new Dal.DbModels.Person
            {
                Href = "https://example.io/people/2",
                FirstName = "Jane",
                LastName = "Smith",
                BirthDate = new DateTimeOffset(1989, 1, 22, 3, 00, 00, TimeSpan.Zero)
            }

        };

        public static void Seed(BeautifulContext context)
        {
            foreach (var person in PeopleData)
            {
                context.People.Add(person);
            }

            context.SaveChanges();
        }
    }
}
