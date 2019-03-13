using Example.Dto;
using Example.Services.Interfaces;
using Example.Web.Controllers;
using Example.Web.Mappers;
using Example.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Example.Web.Tests.Controllers.HorsesControllerTests
{
    public class DetailTests
    {
        private readonly int _id;
        private readonly HorsesController _controller;
        private static Mock<IHorseService> _horseService;

        public DetailTests()
        {
            _id = 999;
            _horseService = new Mock<IHorseService>();
            var logger = new Mock<ILogger<HorsesController>>();

            var horseToHorseDetailMapper = new HorseToHorseDetailMapper();
            var horseToHorseSummaryMapper = new HorseToHorseSummaryMapper();

            _controller = new HorsesController(
                _horseService.Object,
                logger.Object,
                horseToHorseDetailMapper,
                horseToHorseSummaryMapper
            );
        }

        [Fact]
        public void ItReturnsHorse()
        {
            // Arrange
            _horseService.Setup(x => x.Get(_id)).Returns(() => new Horse());

            // Act
            var result = _controller.Detail(_id) as ViewResult;

            // Assert
            _horseService.Verify(mock => mock.Get(_id), Times.Once);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<HorseDetail>(result.ViewData.Model);
        }
    }
}