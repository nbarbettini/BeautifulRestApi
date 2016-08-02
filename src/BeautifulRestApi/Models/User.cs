using System;

namespace BeautifulRestApi.Models
{
    public class User : Resource
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        public ILink Posts { get; set; }
    }
}
