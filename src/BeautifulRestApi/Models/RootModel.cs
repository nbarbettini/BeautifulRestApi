using BeautifulRestApi.Controllers;

namespace BeautifulRestApi.Models
{
    public class RootModel
    {
        public ILink People { get; set; } = PlaceholderLink.ToCollection(PeopleController.Endpoint);
        
        public ILink Orders { get; set; } = PlaceholderLink.ToCollection(OrdersController.Endpoint);
    }
}
