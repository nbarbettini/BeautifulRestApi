using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Models;
using BeautifulRestApi.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BeautifulRestApi.Controllers
{
    [Route(Endpoint)]
    public class PostsController : Controller
    {
        public const string Endpoint = "posts";

        private readonly BeautifulContext _context;
        private readonly PagedCollectionParameters _defaultPagingOptions;

        public PostsController(BeautifulContext context, IOptions<PagedCollectionParameters> defaultPagingOptions)
        {
            _context = context;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        // todo
        //[HttpGet]
        //public async Task<IActionResult> Get(PagedCollectionParameters parameters)
        //{
        //    var getAllQuery = new GetAllOrdersQuery(_context, Endpoint, _defaultPagingOptions);
        //    var results = await getAllQuery.Execute(parameters);

        //    // Attach form definitions for discoverability
        //    results.Forms = new[] { Form.FromModel<PersonCreateModel>(Endpoint, "POST", "create-form") };

        //    return new ObjectResult(results);
        //}

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var query = new GetPostQuery(_context);
            var post = await query.Execute(id);

            return post == null
                ? new NotFoundResult() as ActionResult
                : new ObjectResult(post);
        }
    }
}
