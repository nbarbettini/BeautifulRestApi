using System;

namespace BeautifulRestApi.DbModels
{
    public class DbPost
    {
        public string Id { get; set; } = IdGenerator.NewId();

        public string UserId { get; set; }

        public DbUser User { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Content { get; set; }
    }
}
