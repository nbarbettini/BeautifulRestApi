using BeautifulRestApi.Controllers;

namespace BeautifulRestApi.Models
{
    public class RootModel
    {
        public ILink Users { get; set; } = PlaceholderLink.ToCollection(UsersController.Endpoint);
        
        public ILink Posts { get; set; } = PlaceholderLink.ToCollection(PostsController.Endpoint);
    }
}
