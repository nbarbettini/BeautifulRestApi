using System;
using BeautifulRestApi.Dal;
using BeautifulRestApi.Filters;
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
            services.AddTransient<IResultEnricher, ResourceEnricher>();
            services.AddTransient<ResultEnrichingFilter>();

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ResultEnrichingFilter));
            });

            services.AddDbContext<BeautifulContext>(opt => opt.UseInMemoryDatabase());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<BeautifulContext>();
            TestData.Seed(context);

            app.UseMvc(opt =>
            {
                opt.MapRoute("default", "{controller}/{id?}");
            });
        }
    }
}
