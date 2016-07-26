using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautifulRestApi.Models
{
    public static class CollectionFactory
    {
        public static Collection<T> Create<T>(string href, IEnumerable<T> items)
        {
            var collection = new Collection<T>
            {
                Meta = {Href = href},
                Items = items.ToArray()
            };

            return collection;
        }
    }
}
