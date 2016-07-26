using System;

namespace BeautifulRestApi.Models
{
    public class PersonResponse : Resource
    {
        public PersonResponse(string href, string firstName, string lastName, DateTimeOffset birthDate)
            : base(href)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public DateTimeOffset BirthDate { get; }
    }
}
