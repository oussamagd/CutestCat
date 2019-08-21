//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;
//using CareApi;
//using CareApi.Usecases.Orders;
//using CareApiTests.UsescasesMock;
//using FluentAssertions;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.AspNetCore.TestHost;
//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Xunit;

//namespace CareApiTests.Infrastructure.Controllers
//{
//    public class OrdersControllerWithIdTest : IClassFixture<WebApplicationFactory<Startup>>
//    {
//        public OrdersControllerWithIdTest(WebApplicationFactory<Startup> factory)
//        {
//            _factory = factory;
//            _client = _factory.WithWebHostBuilder(builder =>
//            {
//                builder.ConfigureTestServices(services => { services.AddScoped<IGetOrders, GetOrdersMock>(); });
//            }).CreateClient();
//        }

//        private readonly HttpClient _client;


//        private readonly WebApplicationFactory<Startup> _factory;

//        [Fact]
//        public async void TestGet_WhenCalledAccountWithOrder_ReturnsOkWithOrder()
//        {
//            // When
//            var message = new HttpRequestMessage(HttpMethod.Get, "/accounts/2/orders");
//            message.Headers.Add("Cookie", "UID=value1; SID=value2;.AUTH=value2");
//            var httpResponseMessage = await _client.SendAsync(message);

//            // Then
//            var httpBody = await httpResponseMessage.Content.ReadAsStringAsync();

//            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
//            httpResponseMessage.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");

//            var settings = new JsonSerializerSettings { DateParseHandling = DateParseHandling.None };
//            var result = JsonConvert.DeserializeObject<JArray>(httpBody, settings);


//            result.Count.Should().Be(2);
//            result[0]["id"].Value<int>().Should().Be(1);
//            result[0]["reference"].Value<string>().Should().Be("UUID-435");
//            result[0]["orderDate"].Value<string>().Should().Be("2019-05-02T15:10:25Z");
//            result[0]["status"].Value<string>().Should().Be("SHIPPED");
//            result[0]["type"].Value<string>().Should().Be("STANDARD");
//            result[0]["userReference"].Value<string>().Should().Be("123456");
//            result[0]["sellerName"].Value<string>().Should().Be("AwesomeShop");
//            result[0]["articles"][0]["id"].Value<long>().Should().Be(42);
//            result[0]["articles"][0]["quantity"].Value<int>().Should().Be(10);
//            result[0]["articles"][0]["label"].Value<string>().Should().Be("just a book");
//            result[0]["articles"][0]["smallPictureUrl"].Value<string>().Should().Be("small.png");
//            result[0]["articles"][0]["largePictureUrl"].Value<string>().Should().Be("large.png");
//            result[0]["articles"][0]["relativeUrl"].Value<string>().Should().Be("relativeUrl");
//            result[0]["articles"][0]["isAvailable"].Value<bool>().Should().Be(false);
//            result[0]["articles"][0]["isActive"].Value<bool>().Should().Be(true);
//            result[0]["articles"][0]["type"].Value<string>().Should().Be("EBOOK");
//            result[0]["articles"][0]["category"].Value<string>().Should().Be("CHILDCARE");
//            result[0]["billingAddress"]["addressLine"].Value<string>().Should().Be("2 rue de test");
//            result[0]["billingAddress"]["firstName"].Value<string>().Should().Be("coolName");
//            result[0]["billingAddress"]["zipCode"].Value<string>().Should().Be("75002");
//            result[0]["billingAddress"]["country"].Value<string>().Should().Be("France");
//            result[0]["billingAddress"]["complementaryAddress"][0].Value<string>().Should().Be("AddressLine2 compte 1");
//            result[0]["billingAddress"]["complementaryAddress"][1].Value<string>().Should().Be("AddressLine3 compte 1");
//            result[0]["shippingAddress"]["addressLine"].Value<string>().Should().Be("1 rue de test");
//            result[0]["shippingAddress"]["city"].Value<string>().Should().Be("ivry sur seine");
//            result[0]["shippingAddress"]["relayId"].Value<int>().Should().Be(6);
//            result[0]["shippingAddress"]["phone"].Value<string>().Should().Be("0646132054");
//            result[0]["payments"][1]["creditCard"]["cardType"].Value<string>().Should().Be("FNAC");
//            result[0]["payments"][1]["creditCard"]["expirationDate"].Value<string>().Should().Be("11/25");
//            result[0]["payments"][1]["creditCard"]["cardNumber"].Value<string>().Should().Be("xxxxx3112");
//            result[0]["payments"][1]["creditCard"]["holderName"].Value<string>().Should().Be("Sophie 601");
//            result[0]["payments"][0]["paymentMethod"].Value<string>().Should().Be("CHECK");
//            result[0]["payments"][0]["amount"].Value<decimal>().Should().Be(50);
//            result[1]["id"].Value<int>().Should().Be(2);
//        }

//        [Fact]
//        public async void TestGet_WhenCalledAccountWithoutOrder_ReturnsOkResultEmpty()
//        {
//            // When
//            var message = new HttpRequestMessage(HttpMethod.Get, "/accounts/1/orders");
//            message.Headers.Add("Cookie", "UID=value1; SID=value2;.AUTH=value2");
//            var httpResponseMessage = await _client.SendAsync(message);

//            // Then
//            var httpBody = await httpResponseMessage.Content.ReadAsStringAsync();

//            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
//            httpResponseMessage.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");

//            var result = JArray.Parse(httpBody);
//            result.Count.Should().Be(0);
//        }

//        [Fact]
//        public async void TestGet_WhenCalledWithoutCookie_ReturnsUnauthorized()
//        {
//            // When
//            var httpResponseMessage = await _client.GetAsync("/accounts/1/orders");

//            // Then
//            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
//        }
//    }
//}
