using BeautifulRestApi.Controllers;

namespace BeautifulRestApi.Models
{
    public class RootModel : Resource
    {
        public RootModel()
        {
            Meta = new PlaceholderLink()
            {
                Href = "/",
                Method = "GET"
            };
        }

        public ILink Users { get; set; } = PlaceholderLink.ToCollection(UsersController.Endpoint);
        
        public ILink Posts { get; set; } = PlaceholderLink.ToCollection(PostsController.Endpoint);
    }
}
