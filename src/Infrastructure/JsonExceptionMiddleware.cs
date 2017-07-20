using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BeautifulRestApi.Infrastructure
{
    public sealed class JsonExceptionMiddleware
    {
        public const string DefaultErrorMessage = "A server error occurred.";

        private readonly IHostingEnvironment _env;
        private readonly JsonSerializer _serializer;

        public JsonExceptionMiddleware(IHostingEnvironment env)
        {
            _env = env;

            _serializer = new JsonSerializer();
            _serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (ex == null) return;

            var error = BuildError(ex, _env);

            using (var writer = new StreamWriter(context.Response.Body))
            {
                _serializer.Serialize(writer, error);
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }

        private static ApiError BuildError(Exception ex, IHostingEnvironment env)
        {
            var error = new ApiError();

            if (env.IsDevelopment())
            {
                error.Message = ex.Message;
                error.Detail = ex.StackTrace;
            }
            else
            {
                error.Message = DefaultErrorMessage;
                error.Detail = ex.Message;
            }

            return error;
        }
    }
}
