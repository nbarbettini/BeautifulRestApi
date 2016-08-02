using System;
using System.Collections.Generic;

namespace BeautifulRestApi.Dal.DbModels
{
    public class User
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        public List<Post> Posts { get; set; }
    }
}