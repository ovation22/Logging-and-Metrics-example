using Example.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Example.Web.Tests.Controllers.HomeControllerTests
{
    public class PrivacyTests
    {
        [Fact]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
