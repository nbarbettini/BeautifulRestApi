namespace BeautifulRestApi.Models
{
    public class CollectionLink : Link
    {
        public CollectionLink(string path, string parameters = null)
            : base(path)
        {
            Parameters = parameters;
        }

        public string Parameters { get; }
    }
}
