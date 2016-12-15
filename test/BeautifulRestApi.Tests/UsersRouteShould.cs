using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Xunit;

namespace BeautifulRestApi.Tests
{
    public class UsersRouteShould
    {
        private readonly HttpClient _client;

        public UsersRouteShould()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task ReturnCollection()
        {
            var response = await _client.GetAsync("/users");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            dynamic data = JObject.Parse(await response.Content.ReadAsStringAsync());
            Assert.True(data.items.Count > 0);
        }

        [Fact]
        public async Task ReturnNotFoundForUnknownId()
        {
            var response = await _client.GetAsync("/users/100abc");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
