using System;
using Microsoft.AspNetCore.Mvc;

namespace BeautifulRestApi.Controllers
{
    public class GetConversationByIdParameters
    {
        [FromRoute]
        public Guid ConversationId { get; set; }
    }
}