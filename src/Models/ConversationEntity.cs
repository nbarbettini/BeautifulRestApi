using System;

namespace BeautifulRestApi.Models
{
    public sealed class ConversationEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        // TODO
        // public User Author {get;set;}
    }
}
