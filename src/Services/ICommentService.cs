using System;
using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Services
{
    public interface ICommentService
    {
        Task<CommentResource> GetCommentAsync(Guid id, CancellationToken ct);

        Task<Page<CommentResource>> GetCommentsAsync(
            Guid? conversationId,
            PagingOptions pagingOptions,
            CancellationToken ct);
    }
}
