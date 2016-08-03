using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Queries
{
    public class AddForms<T> : ITransformation<T, T>
        where T : Resource
    {
        public Form[] Forms { get; set; }

        public Task<T> ExecuteAsync(T input, CancellationToken cancellationToken = new CancellationToken())
        {
            input.Forms = (input.Forms ?? new Form[0]).Concat(Forms).ToArray();

            return Task.FromResult(input);
        }
    }
}
