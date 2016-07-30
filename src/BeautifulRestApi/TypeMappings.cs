using BeautifulRestApi.Controllers;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi
{
    public class TypeMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Dal.DbModels.Person, Person>()
                .MapWith(src => new Person
                {
                    Meta = PlaceholderLink.ToResource(PeopleController.Endpoint, src.Id, "GET", null),
                    Orders = PlaceholderLink.ToCollection($"{PeopleController.Endpoint}/{src.Id}/orders", "GET", null),
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    BirthDate = src.BirthDate
                });

            config.ForType<Dal.DbModels.Order, Order>()
                .MapWith(src => new Order
                {
                    Meta = PlaceholderLink.ToResource(OrdersController.Endpoint, src.Id, "GET", null),
                    Person = PlaceholderLink.ToResource(PeopleController.Endpoint, src.PersonId, "GET", null),
                    CreatedAt = src.CreatedAt,
                    Total = src.Total
                });
        }
    }
}

