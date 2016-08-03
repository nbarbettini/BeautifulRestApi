using System;
using System.Collections.Generic;
using System.Linq;
using BeautifulRestApi.DbModels;

namespace BeautifulRestApi.TestData
{
    public class TestPosts : AbstractTestData<DbPost>
    {
        public TestPosts(int numberOfPosts, IReadOnlyList<string> userIds)
        {
            Data = Generate(userIds).Take(numberOfPosts).ToArray();
        }

        private static IEnumerable<DbPost> Generate(IReadOnlyList<string> userIds)
        {
            var random = new Random();

            while (true)
            {
                yield return new DbPost
                {
                    UserId = userIds[random.Next(0, userIds.Count - 1)],
                    Content = LoremNET.Lorem.Sentence(random.Next(15)),
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
