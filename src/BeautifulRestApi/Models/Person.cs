using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautifulRestApi.Models
{
    public class Person : Resource
    {
        public Person(string href)
            : base(href)
        { }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset BirthDate { get; set; }
    }
}
