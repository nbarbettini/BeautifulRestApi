namespace BeautifulRestApi.Models
{
    public class PagedCollection<T> : Collection<T>
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public int Size { get; set; }

        public ILink First { get; set; }

        public ILink Previous { get; set; }

        public ILink Next { get; set; }

        public ILink Last { get; set; }
    }
}
