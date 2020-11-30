using FoxInvaders.Controllers;
using FoxInvaders.Services;
using NSubstitute;
using Xunit;

namespace FoxInvaders.Tests
{
    public class HelloControllerTests
    {
        [Fact]
        public void Get_ShouldCallTheService()
        {
            // Arrange
            var mockService = Substitute.For<IHelloService>();
            var controller = new HelloController(mockService);
            string expectedResult = "test";
            mockService.SayHello().Returns(expectedResult);

            // Act
            var result = controller.Get().Value;

            // Assert
            Assert.Equal(expectedResult, result);
            mockService.Received(requiredNumberOfCalls: 1).SayHello();
        }
    }
}
