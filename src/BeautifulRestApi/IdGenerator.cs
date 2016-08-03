using System;

namespace BeautifulRestApi
{
    public static class IdGenerator
    {
        public static string NewId() => Guid.NewGuid().ToString().Replace("-", string.Empty);
    }
}
