using System;

namespace BeautifulRestApi.Models
{
    public class PersonResponse : Resource
    {
        public PersonResponse(Link href, string firstName, string lastName, DateTimeOffset? birthDate)
        {
            Meta = href;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public DateTimeOffset? BirthDate { get; }
    }
}
