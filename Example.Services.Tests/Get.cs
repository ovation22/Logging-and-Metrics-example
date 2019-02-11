using Xunit;

namespace Example.Services.Tests
{
    public class Get : TestBase
    {
        private readonly int _id;

        public Get()
        {
            _id = 999;
        }

        [Fact]
        public void ItReturnsHorse()
        {
            // Arrange
            // Act
            var horse = HorseService.Get(_id);

            // Assert
            Assert.IsAssignableFrom<Dto.Horse>(horse);
        }
    }
}
