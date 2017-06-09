using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            dynamic collection = JObject.Parse(await response.Content.ReadAsStringAsync());
            Assert.True(collection.value.Count > 0);
        }

        [Fact]
        public async Task ReturnNotFoundForUnknownId()
        {
            var response = await _client.GetAsync("/users/100");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ReturnUser()
        {
            var response = await _client.GetAsync("/users/17");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            dynamic user = JObject.Parse(await response.Content.ReadAsStringAsync());
            Assert.Equal("Luke", user.firstName);
            Assert.Equal("Skywalker", user.lastName);
        }
    }
}
