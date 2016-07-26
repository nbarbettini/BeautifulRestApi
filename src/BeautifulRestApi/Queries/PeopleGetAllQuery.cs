using System;
using System.Linq;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public class PeopleGetAllQuery : QueryBase<CollectionResponse<PersonResponse>>
    {
        public PeopleGetAllQuery(BeautifulContext context)
            : base(context)
        {
        }

        public CollectionResponse<PersonResponse> Execute(string baseHref)
        {
            var items = Context.People
                .Select(src => new PersonResponse(ConstructResourceHref(baseHref, "people", src.Id), src.FirstName, src.LastName, src.BirthDate))
                .ToArray();

            return new CollectionResponse<PersonResponse>(baseHref, items);
        }
    }
}
