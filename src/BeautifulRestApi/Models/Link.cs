using System;

namespace BeautifulRestApi.Models
{
    public class Link : Resource
    {
        public Link(string destinationUri)
            : base(destinationUri)
        {
            if (string.IsNullOrEmpty(destinationUri))
            {
                throw new ArgumentNullException(nameof(destinationUri));
            }
        }
    }
}
