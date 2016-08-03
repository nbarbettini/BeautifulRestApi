using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.DbModels;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class ProjectToPagedCollection<TSource, TResult> : ITransformation<IQueryable<TSource>, PagedCollection<TResult>>
    {
        public string Endpoint { get; set; }

        public PagedCollectionParameters DefaultPagingParameters { get; set; }

        public PagedCollectionParameters PagingParameters { get; set; }

        public ILink Meta { get; set; }

        public Task<PagedCollection<TResult>> ExecuteAsync(IQueryable<TSource> input, CancellationToken cancellationToken = new CancellationToken())
        {
            if (DefaultPagingParameters == null)
            {
                throw new ArgumentNullException(nameof(DefaultPagingParameters));
            }

            var collectionFactory = new PagedCollectionFactory<TResult>(Meta);

            return collectionFactory.CreateFrom(
                input.ProjectToType<TResult>(),
                PagingParameters.Offset ?? DefaultPagingParameters.Offset.Value,
                PagingParameters.Limit ?? DefaultPagingParameters.Limit.Value);
        }
    }
}
