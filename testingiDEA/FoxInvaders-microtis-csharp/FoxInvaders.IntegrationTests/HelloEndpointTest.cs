using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FoxInvaders.IntegrationTests
{
    public class HelloEndpointTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient httpClient;

        public HelloEndpointTest(CustomWebApplicationFactory factory)
        {
            factory.ConfigureTestServices(services =>
            {
                // You can mock services here if you have to
                // e.g.
                //var mockService = Substitute.For<IHelloService>();
                //string expectedResult = "test";
                //mockService.SayHello().Returns(expectedResult);

                //services.AddTransient<IHelloService>(_ => mockService);
            });

            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Get_ShouldReturnHelloWorld()
        {
            var response = await httpClient.GetAsync("/api/hello");
            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Hello world!", result);
        }
    }
}
