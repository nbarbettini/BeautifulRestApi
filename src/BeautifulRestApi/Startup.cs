using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            // Add framework services.
            services.AddMvc();

            services.AddDbContext<BeautifulContext>(opt => opt.UseInMemoryDatabase());

            // Add business services
            services.AddTransient<IPeopleService, PeopleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<BeautifulContext>();
            SeedTestData(context);

            app.UseMvc();
        }

        private static void SeedTestData(BeautifulContext context)
        {
            context.People.Add(new Dal.DbModels.Person
            {
                Href = "https://example.io/people/1",
                FirstName = "Bob",
                LastName = "Smith",
                BirthDate = new DateTimeOffset(1985, 6, 12, 15, 00, 00, TimeSpan.Zero)
            });
            context.People.Add(new Dal.DbModels.Person
            {
                Href = "https://example.io/people/2",
                FirstName = "Jane",
                LastName = "Smith",
                BirthDate = new DateTimeOffset(1989, 1, 22, 3, 00, 00, TimeSpan.Zero)
            });
            context.SaveChanges();
        }
    }
}
