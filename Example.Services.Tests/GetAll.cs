using System.Collections.Generic;
using Xunit;

namespace Example.Services.Tests
{
    public class GetAll : TestBase
    {
        [Fact]
        public void ItReturnsCollectionOfHorses()
        {
            // Arrange
            // Act
            var horses = HorseService.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Dto.Horse>>(horses);
        }
    }
}
