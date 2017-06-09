using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautifulRestApi.Models
{
    public class User : Resource
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
