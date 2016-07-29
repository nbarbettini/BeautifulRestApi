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

        public DbSet<Person> People { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
