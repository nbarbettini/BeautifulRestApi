using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace BeautifulRestApi.Tests
{
    public class PostsRouteShould
    {
        private readonly HttpClient _client;

        public PostsRouteShould()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task ReturnNotFoundForUnknownId()
        {
            var response = await _client.GetAsync("/posts/100abc");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
