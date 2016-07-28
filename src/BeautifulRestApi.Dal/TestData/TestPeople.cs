using System;
using BeautifulRestApi.Dal.DbModels;

namespace BeautifulRestApi.Dal.TestData
{
    public class TestPeople : AbstractTestData<Person>
    {
        public TestPeople()
        {
            Data = new[]
            {
                new Person
                {
                    Id = "1abc",
                    FirstName = "Bob",
                    LastName = "Smith",
                    BirthDate = new DateTimeOffset(1985, 6, 12, 15, 00, 00, TimeSpan.Zero)
                },
                new Person
                {
                    Id = "2def",
                    FirstName = "Jane",
                    LastName = "Smith",
                    BirthDate = new DateTimeOffset(1989, 1, 22, 3, 00, 00, TimeSpan.Zero)
                }

            };
        }
    }
}
