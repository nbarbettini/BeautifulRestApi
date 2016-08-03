using System;
using System.Collections.Generic;
using System.Linq;
using BeautifulRestApi.DbModels;

namespace BeautifulRestApi.TestData
{
    public class TestUsers : AbstractTestData<DbUser>
    {
        public TestUsers(int numberOfUsers)
        {
            Data = Generate().Take(numberOfUsers).ToArray();
        }

        private static readonly string[] GivenNames =
        {
            "John", "Jane", "Nate", "Mitch", "Brian", "Amy", "Bob", "Susan", "Jessica", "Steve"
        };

        private static readonly string[] Surnames =
        {
            "Smith", "Testerman", "Johnson", "Jones", "Garcia", "Cole", "Lee"
        };

        private static IEnumerable<DbUser> Generate()
        {
            var random = new Random();

            while (true)
            {
                yield return new DbUser {
                    FirstName = GivenNames[random.Next(GivenNames.Length - 1)],
                    LastName = Surnames[random.Next(Surnames.Length - 1)],
                    BirthDate = new DateTimeOffset(
                        year: random.Next(1950, 1999),
                        month: random.Next(1, 12),
                        day: random.Next(1, 29),
                        hour: random.Next(24),
                        minute: random.Next(60),
                        second: random.Next(60),
                        offset: TimeSpan.Zero)
                };
            }
        }
    }
}
