using FoxInvaders.Services;
using Xunit;

namespace FoxInvaders.Tests
{
    public class HelloServiceTests
    {
        [Fact]
        public void GetShouldReturnHelloWorld()
        {
            // Arrange
            var service = new HelloService();

            // Act
            var result = service.SayHello();

            // Assert
            Assert.Equal("Hello world!", result);
        }
    }
}
