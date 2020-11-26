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
            LoginRequestDto userNull = null;
            var mockUserService = new Mock<IJwtAuthenticationService>();
            mockUserService.Setup(m => m.Authenticate(null, null)).Returns(() => null);
            var controller = new CustomerController(mockUserService.Object);
            var result = controller.Login(null);
            
            Assert.IsType<Task<ActionResult>>(result);
        }
    }
}