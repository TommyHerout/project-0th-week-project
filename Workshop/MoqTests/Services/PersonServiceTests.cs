using Microsoft.AspNetCore.Mvc;
using Moq;
using Workshop.Controllers;
using Workshop.Models;
using Workshop.Models.Dto.Requests;
using Workshop.Services.Interfaces;
using Xunit;

namespace MoqTests
{
    public class PersonServiceTests
    {
        [Fact]
        public void Login_Returns_Unauthorized()
        {
            var user = new LoginRequestDto {Username = "Tommy", Password = "tommy"};
            var mockUserService = new Mock<IJwtAuthenticationService>();
            mockUserService.Setup(m => m.Authenticate(null,  null)).Returns(() => null);
            var controller = new CustomerController(null, null, mockUserService.Object, null);
            var result = controller.Login(user);
            Assert.IsType<UnauthorizedObjectResult>(result.Result);
        }
        
        [Fact]
        public void Login_Returns_Ok()
        {
            var user = new LoginRequestDto("Tommy", "tommy");
            var mockUserService = new Mock<IJwtAuthenticationService>();
            mockUserService.Setup(m => m.Authenticate(user.Username, user.Password)).ReturnsAsync("thisShouldBeSomeApiKey");
            var controller = new CustomerController(null, null, mockUserService.Object, null);
            var result = controller.Login(user);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Register_Returns_NoInput()
        {
            var user = new RegisterRequestDto();
            var mockUserService = new Mock<IPersonService>();
            mockUserService.Setup(m => m.Register(null)).Returns(() => null);
            var controller = new CustomerController(mockUserService.Object, null, null, null);
            var result = controller.Register(user);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Register_Returns_Already_Exists()
        {
            var person = new Person{Username = "tom"};
            var user = new RegisterRequestDto{Username = "tom"};
            var mockUserService = new Mock<IPersonService>();
            mockUserService.Setup(m => m.FindPersonByUsername(person.Username)).ReturnsAsync(() => person);
            var controller = new CustomerController(mockUserService.Object, null, null, null);
            var result = controller.Register(user);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}