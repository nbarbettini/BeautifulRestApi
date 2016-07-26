using System;

namespace BeautifulRestApi.Dal.DbModels
{
    public class Person
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset BirthDate { get; set; }
    }
}