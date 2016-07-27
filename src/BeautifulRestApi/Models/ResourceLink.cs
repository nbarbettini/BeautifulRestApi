namespace BeautifulRestApi.Models
{
    public class ResourceLink : Link
    {
        public ResourceLink(string path, string id)
            : base(path)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
