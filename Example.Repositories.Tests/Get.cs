using Moq;
using Xunit;

namespace Example.Repositories.Tests
{
    public class Get : TestBase
    {
        [Fact]
        public void ItExists()
        {
            // Arrange
            // Act
            var thing = Repository.Get(It.IsAny<int>());

            // Assert
            Assert.Null(thing);
            MockSet.Verify(set => set.Find(It.IsAny<int>()), Times.Once);
        }
    }
}
