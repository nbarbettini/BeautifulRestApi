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
                    Meta = new ResourceLink(PeopleController.Endpoint, src.Id),
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    BirthDate = src.BirthDate
                });
        }
    }
}
