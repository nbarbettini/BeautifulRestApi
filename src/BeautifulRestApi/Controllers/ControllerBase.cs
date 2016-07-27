using System;
using BeautifulRestApi.Dal;
using Microsoft.AspNetCore.Mvc;

namespace BeautifulRestApi.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly BeautifulContext DataContext;

        protected ControllerBase(BeautifulContext context)
        {
            DataContext = context;
        }

        protected string RootHref => new UriBuilder()
        {
            Scheme = Request.Scheme,
            Host = Request.Host.Host,
            Port = Request.Host.Port ?? 80
        }.ToString();
    }
}
