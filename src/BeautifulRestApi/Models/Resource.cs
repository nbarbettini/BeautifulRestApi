using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautifulRestApi.Models
{
    public abstract class Resource
    {
        public Metadata Meta { get; set; }
    }
}
