using System;

namespace BeautifulRestApi.Models
{
    public class Person : Resource
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        public ILink Orders { get; set; }
    }
}
