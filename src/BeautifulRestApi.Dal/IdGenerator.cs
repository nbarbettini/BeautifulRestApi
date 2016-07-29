using System;

namespace BeautifulRestApi.Dal
{
    public static class IdGenerator
    {
        public static string GetId() => Guid.NewGuid().ToString().Replace("-", string.Empty);
    }
}
