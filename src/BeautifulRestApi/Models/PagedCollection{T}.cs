using System.Collections;

namespace BeautifulRestApi.Models
{
    public abstract class PagedCollection : Resource, ICollection
    {
        public abstract IEnumerator GetEnumerator();

        public Link First { get; set; }

        public Link Previous { get; set; }

        public Link Next { get; set; }

        public Link Last { get; set; }
    }

    public class PagedCollection<T> : PagedCollection
    {
        public T[] Items { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public int Size { get; set; }

        public override IEnumerator GetEnumerator() => Items.GetEnumerator();
    }
}
