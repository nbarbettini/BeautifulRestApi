using System;
using System.Threading.Tasks;
using BeautifulRestApi.DbModels;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class CreatePostQuery
    {
        private readonly BeautifulContext _context;
        private readonly TypeAdapterConfig _typeAdapterConfig;

        public CreatePostQuery(BeautifulContext context, TypeAdapterConfig typeAdapterConfig)
        {
            _context = context;
            _typeAdapterConfig = typeAdapterConfig;
        }

        public async Task<Tuple<string, Post>> Execute(PostCreateModel model)
        {
            var entry = _context.Posts.Add(new DbPost
            {
                UserId = model.UserId,
                CreatedAt = DateTimeOffset.UtcNow,
                Content = model.Content
            });

            await _context.SaveChangesAsync();

            return new Tuple<string, Post>(
                entry.Entity.Id,
                entry.Entity.Adapt<Post>(_typeAdapterConfig));
        }
    }
}
