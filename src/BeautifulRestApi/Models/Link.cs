using System;

namespace BeautifulRestApi.Models
{
    public class Link : Resource
    {
        public Link(string destinationUri, string relation = null)
            : base(destinationUri)
        {
            if (string.IsNullOrEmpty(destinationUri))
            {
                throw new ArgumentNullException(nameof(destinationUri));
            }

            if (!string.IsNullOrEmpty(relation))
            {
                Meta.Relations = new[] {relation};
            }
        }
    }
}
