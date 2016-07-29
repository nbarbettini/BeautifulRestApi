using System;

namespace BeautifulRestApi.Dal.DbModels
{
    public class Order
    {
        public string Id { get; set; }

        public string PersonId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public double Total { get; set; }
    }
}
