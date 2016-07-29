using System;

namespace BeautifulRestApi.Models
{
    public class Order : Resource
    {
        public ILink Person { get; set; }

        public double Total { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
