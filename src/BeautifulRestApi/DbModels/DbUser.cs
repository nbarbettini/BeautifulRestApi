using System;
using System.Collections.Generic;

namespace BeautifulRestApi.DbModels
{
    public class DbUser
    {
        public string Id { get; set; } = IdGenerator.NewId();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        public List<DbPost> Posts { get; set; }
    }
}
