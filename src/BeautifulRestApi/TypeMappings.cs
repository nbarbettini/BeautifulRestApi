using BeautifulRestApi.Controllers;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi
{
    public class PersonTypeMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Dal.DbModels.Person, Person>()
                .MapWith(src => new Person
                {
                    Meta = PlaceholderLink.ToResource(PeopleController.Endpoint, src.Id, "GET", null),
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    BirthDate = src.BirthDate
                });
        }
    }
}
