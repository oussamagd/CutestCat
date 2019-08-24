using CutestCat;
using CutestCat.Business;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using Xunit;
namespace CutestCatTest.Controller
{
    public class CatControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        public CatControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => { services.AddScoped<ICatBusiness, CatBusinessMock>(); });
            }).CreateClient();
        }

        private readonly HttpClient _client;


        private readonly WebApplicationFactory<Startup> _factory;

        [Fact]
        public async void TestGetCandidate_ReturnsOkWithCandidates()
        {
            // When
            var message = new HttpRequestMessage(HttpMethod.Get, "/api/Cat/Vote/Candidates");
            var httpResponseMessage = await _client.SendAsync(message);

            // Then
            var httpBody = await httpResponseMessage.Content.ReadAsStringAsync();

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            httpResponseMessage.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");

            var result = JArray.Parse(httpBody);
            result.Count.Should().Be(2);
            result[0]["reference"].Value<string>().Should().Be("Ugly");
            result[0]["url"].Value<string>().Should().Be("ugly.com");
        }

        [Fact]
        public async void TestGetCat_ReturnsOkWithCandidates()
        {
            // When
            var message = new HttpRequestMessage(HttpMethod.Get, "/api/Cat");
            var httpResponseMessage = await _client.SendAsync(message);

            // Then
            var httpBody = await httpResponseMessage.Content.ReadAsStringAsync();

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            httpResponseMessage.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");

            var result = JArray.Parse(httpBody);
            result.Count.Should().Be(4);
            result[0]["reference"].Value<string>().Should().Be("Ugly");
            result[0]["url"].Value<string>().Should().Be("ugly.com");
            result[0]["lostVoteCount"].Value<int>().Should().Be(10);
            result[0]["winVoteCount"].Value<int>().Should().Be(1);
        }
    }
}
