using System;
using System.Collections.Generic;
using System.Linq;
using BeautifulRestApi.Dal.DbModels;

namespace BeautifulRestApi.Dal.TestData
{
    public class TestOrders : AbstractTestData<Order>
    {
        public TestOrders(int numberOfOrders, string[] personIds)
        {
            Data = Generate(personIds).Take(numberOfOrders).ToArray();
        }

        private static IEnumerable<Order> Generate(string[] personIds)
        {
            var random = new Random();

            while (true)
            {
                yield return new Order
                {
                    Id = IdGenerator.GetId(),
                    PersonId = personIds[random.Next(0, personIds.Length - 1)],
                    Total = Math.Round(random.NextDouble() * 500, 2),
                    CreatedAt = new DateTimeOffset(
                        year: random.Next(2000, DateTimeOffset.Now.Year),
                        month: random.Next(1, 12),
                        day: random.Next(1, 29),
                        hour: random.Next(24),
                        minute: random.Next(60),
                        second: random.Next(60),
                        offset: TimeSpan.Zero)
                };
            }
        }
    }
}
