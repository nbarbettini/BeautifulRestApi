using BeautifulRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<ConversationEntity> Conversations { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }
    }
}
