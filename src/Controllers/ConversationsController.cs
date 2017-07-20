using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using BeautifulRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BeautifulRestApi.Controllers
{
    [Route("/[controller]")]
    public class ConversationsController : Controller
    {
        private readonly IConversationService _conversationService;
        private readonly PagingOptions _defaultPagingOptions;

        public ConversationsController(
            IConversationService conversationService,
            IOptions<PagingOptions> defaultPagingOptionsAccessor)
        {
            _conversationService = conversationService;
            _defaultPagingOptions = defaultPagingOptionsAccessor.Value;
        }

        [HttpGet(Name = nameof(GetConversationsAsync))]
        public async Task<IActionResult> GetConversationsAsync(
            [FromQuery] PagingOptions pagingOptions,
            // todo fromquery sort/search options
            CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var conversations = await _conversationService.GetConversationsAsync(
                pagingOptions, ct);

            var collection = CollectionWithPaging<ConversationResource>.Create(
                Link.ToCollection(nameof(GetConversationsAsync)),
                conversations.Items.ToArray(),
                conversations.TotalSize,
                pagingOptions);

            return Ok(collection);
        }

        [HttpGet("{id}", Name = nameof(GetConversationByIdAsync))]
        public async Task<IActionResult> GetConversationByIdAsync(GetConversationByIdOptions options, CancellationToken ct)
        {
            if (options.Id == Guid.Empty) return NotFound();

            var conversation = await _conversationService.GetConversationAsync(options.Id, ct);
            if (conversation == null) return NotFound();

            return Ok(conversation);
        }
    }
}
