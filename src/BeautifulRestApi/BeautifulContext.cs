using BeautifulRestApi.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi
{
    public class BeautifulContext : DbContext
    {
        public BeautifulContext(DbContextOptions<BeautifulContext> options)
            : base(options)
        {
        }

        public DbSet<DbUser> Users { get; set; }

        public DbSet<DbPost> Posts { get; set; }
    }
}