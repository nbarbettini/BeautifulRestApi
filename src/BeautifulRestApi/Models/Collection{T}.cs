using System.Collections;

namespace BeautifulRestApi.Models
{
    public interface ICollection
    {
        IEnumerator GetEnumerator();
    }

    public class Collection<T> : Resource, ICollection
    {
        public T[] Items { get; set; }

        public IEnumerator GetEnumerator() => Items.GetEnumerator();
    }
}
