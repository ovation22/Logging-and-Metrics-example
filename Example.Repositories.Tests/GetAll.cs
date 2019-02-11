using System.Collections.Generic;
using Xunit;

namespace Example.Repositories.Tests
{
    public class GetAll : TestBase
    {
        [Fact]
        public void ItExists()
        {
            // Arrange

            // Act
            var things = Repository.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Thing>>(things);
        }
    }
}
