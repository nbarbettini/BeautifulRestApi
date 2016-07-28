using System.Collections.Generic;

namespace BeautifulRestApi.Models
{
    public class Form : Collection<FormField>
    {
        public Form(string path, string method, string relation, IEnumerable<FormField> fields)
            : base(new Link(path, new[] { relation }, method), fields)
        {
        }
    }
}
