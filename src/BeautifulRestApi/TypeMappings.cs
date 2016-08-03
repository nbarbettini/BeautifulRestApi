using BeautifulRestApi.Controllers;
using BeautifulRestApi.DbModels;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi
{
    public class TypeMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<DbUser, User>()
                .MapWith(src => new User
                {
                    Meta = PlaceholderLink.ToResource(UsersController.Endpoint, src.Id, "GET", null),
                    Posts = PlaceholderLink.ToCollection(UsersController.Endpoint, "GET", new { id = src.Id, link = PostsController.Endpoint }),
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    BirthDate = src.BirthDate
                });

            config.ForType<DbPost, Post>()
                .MapWith(src => new Post
                {
                    Meta = PlaceholderLink.ToResource(PostsController.Endpoint, src.Id, "GET", null),
                    User = PlaceholderLink.ToResource(UsersController.Endpoint, src.UserId, "GET", null),
                    CreatedAt = src.CreatedAt,
                    Content = src.Content
                });
        }
    }
}

