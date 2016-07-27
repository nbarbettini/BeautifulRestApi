using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautifulRestApi
{
    public static class UrlHelper
    {
        public static string Construct(params string[] tokens)
        {
            var cleanTokens = tokens.Select(t => t.TrimStart('/', '\\').TrimEnd('/', '\\'));

            return string.Join("/", cleanTokens);
        }
    }
}
