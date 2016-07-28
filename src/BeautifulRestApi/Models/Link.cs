namespace BeautifulRestApi.Models
{
    public class Link
    {
        public Link(string href, string[] relations = null, string method = null)
        {
            Href = href;
            Relations = relations;
            Method = method;
        }

        public string Href { get; }

        public string[] Relations { get; }

        public string Method { get; }
    }
}
