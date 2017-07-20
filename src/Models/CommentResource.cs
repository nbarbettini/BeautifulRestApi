using System;

namespace BeautifulRestApi.Models
{
    public sealed class CommentResource : Resource
    {
        public Link Conversation { get; set; }

        // TODO
        // public Link Author {get;set;}

        public string Body { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
