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
    public sealed class DefaultCommentService : ICommentService
    {
        private readonly ApiDbContext _context;

        public DefaultCommentService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<CommentResource> GetCommentAsync(Guid id, CancellationToken ct)
        {
            var entity = await _context
                .Comments
                .SingleOrDefaultAsync(x => x.Id == id, ct);

            return Mapper.Map<CommentResource>(entity);
        }

        public async Task<Page<CommentResource>> GetCommentsAsync(
            Guid? conversationId,
            PagingOptions pagingOptions,
            CancellationToken ct)
        {
            IQueryable<CommentEntity> query = _context.Comments;

            if (conversationId != null)
            {
                query = query.Where(x => x.Conversation.Id == conversationId);
            }

            // todo apply search
            // todo apply sort

            var size = await query.CountAsync(ct);

            var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<CommentResource>()
                .ToArrayAsync(ct);

            return new Page<CommentResource>
            {
                Items = items,
                TotalSize = size
            };
        }
    }
}
