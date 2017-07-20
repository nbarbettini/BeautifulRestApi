using System;
using Microsoft.AspNetCore.Mvc;

namespace BeautifulRestApi.Controllers
{
    public class GetConversationByIdOptions
    {
        [FromRoute]
        public Guid Id { get; set; }
    }
}