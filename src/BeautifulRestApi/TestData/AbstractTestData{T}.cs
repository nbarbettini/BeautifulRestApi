using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.TestData
{
    public abstract class AbstractTestData<T>
        where T : class
    {
        public T[] Data { get; protected set; }

        public void Seed(DbSet<T> dbSet)
        {
            foreach (var item in Data)
            {
                dbSet.Add(item);
            }
        }
    }
}
