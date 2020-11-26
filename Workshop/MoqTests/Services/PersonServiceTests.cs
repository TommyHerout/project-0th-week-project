using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Workshop.Controllers;
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
            mockUserService.Setup(m => m.Authenticate(null, null)).Returns(() => null);
            var controller = new CustomerController(null, null, mockUserService.Object, null);
            var result = controller.Login(user);
            Assert.IsType<UnauthorizedObjectResult>(result.Result);
        }
    }
}