using System;
using Microsoft.AspNetCore.Mvc;

namespace BeautifulRestApi.Controllers
{
    public class GetCommentByIdParameters
    {
        [FromRoute]
        public Guid CommentId { get; set; }
    }
}