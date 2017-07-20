using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeautifulRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautifulRestApi.Services
{
    public sealed class DefaultConversationService : IConversationService
    {
        private readonly ApiDbContext _context;

        public DefaultConversationService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<ConversationResource> GetConversationAsync(Guid id, CancellationToken ct)
        {
            var entity = await _context
                .Conversations
                .SingleOrDefaultAsync(x => x.Id == id, ct);

            return Mapper.Map<ConversationResource>(entity);
        }

        public async Task<Page<ConversationResource>> GetConversationsAsync(
            PagingOptions pagingOptions,
            CancellationToken ct)
        {
            IQueryable<ConversationEntity> query = _context.Conversations;
            // todo apply search
            // todo apply sort

            var size = await query.CountAsync(ct);

            var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<ConversationResource>()
                .ToArrayAsync(ct);

            return new Page<ConversationResource>
            {
                Items = items,
                TotalSize = size
            };
        }
    }
}
