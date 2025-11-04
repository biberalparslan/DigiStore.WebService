using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DigiStore.WebService.IntegrationTests
{
    public class ProductApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProductApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_All_Products_Returns_OK()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/product");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
