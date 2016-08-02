using BeautifulRestApi.Dal.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Dal
{
    public class BeautifulContext : DbContext
    {
        public BeautifulContext(DbContextOptions<BeautifulContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}
