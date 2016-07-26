using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public class PeopleGetQuery : QueryBase<PersonResponse>
    {
        private readonly string _baseHref;

        public PeopleGetQuery(BeautifulContext context, string baseHref)
            : base(context)
        {
            _baseHref = baseHref;
        }

        public PersonResponse Execute(string id)
        {
            var p = Context.People.SingleOrDefault(x => x.Id == id);

            return p == null
                ? null
                : new PersonResponse(
                    ConstructResourceHref(_baseHref, "people", p.Id),
                    p.FirstName,
                    p.LastName,
                    p.BirthDate);
        }
    }
}
