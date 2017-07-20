using System;
using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Services
{
    public interface IConversationService
    {
        Task<ConversationResource> GetConversationAsync(Guid id, CancellationToken ct);

        Task<Page<ConversationResource>> GetConversationsAsync(
            PagingOptions pagingOptions,
            CancellationToken ct);
    }
}
