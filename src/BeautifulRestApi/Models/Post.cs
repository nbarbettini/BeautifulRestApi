using System;

namespace BeautifulRestApi.Models
{
    public class Post : Resource
    {
        public ILink User { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
