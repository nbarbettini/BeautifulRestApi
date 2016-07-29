using System.Linq;
using System.Reflection;
using BeautifulRestApi.Controllers;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Dal.TestData;
using BeautifulRestApi.Filters;
using BeautifulRestApi.Models;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BeautifulRestApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BeautifulContext>(opt => opt.UseInMemoryDatabase());

            services.AddSingleton(Options.Create(new PagedCollectionParameters
            {
                Limit = 25,
                Offset = 0
            }));

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(LinkRewritingFilter));
                //options.Filters.Add(typeof(JsonExceptionFilter));
            });

            // Add POCO mapping configurations
            TypeAdapterConfig.GlobalSettings.Scan(typeof(Startup).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Seed data store with test data
            var context = app.ApplicationServices.GetService<BeautifulContext>();
            
            var fakePeople = new TestPeople(26);
            var fakeOrders = new TestOrders(100, fakePeople.Data.Select(p => p.Id).ToArray());

            fakePeople.Seed(context.People);
            fakeOrders.Seed(context.Orders);

            context.SaveChanges();

            app.UseMvc(opt =>
            {
                opt.MapRoute("default", "{controller}/{id?}");
            });
        }
    }
}
