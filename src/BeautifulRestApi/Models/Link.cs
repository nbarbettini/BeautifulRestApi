namespace BeautifulRestApi.Models
{
    public abstract class Link
    {
        protected Link(string href)
        {
            Href = href;
        }

        public string Href { get; }
    }
}
