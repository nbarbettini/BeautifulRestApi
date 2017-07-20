namespace BeautifulRestApi.Models
{
    public class RootResource : Resource
    {
        public Link Conversations { get; set; }

        public Link Comments { get; set; }
    }
}
