using System.Collections.Generic;
using Example.Services.Interfaces;
using Example.Web.Controllers;
using Example.Web.Mappers;
using Example.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Example.Web.Tests.Controllers.HorsesControllerTests
{
    public class IndexTests
    {
        private readonly HorsesController _controller;
        private static Mock<IHorseService> _horseService;

        public IndexTests()
        {
            _horseService = new Mock<IHorseService>();

            var horseToHorseDetailMapper = new HorseToHorseDetailMapper();
            var horseToHorseSummaryMapper = new HorseToHorseSummaryMapper();

            _controller = new HorsesController(_horseService.Object,
                horseToHorseSummaryMapper,
                horseToHorseDetailMapper);
        }

        [Fact]
        public void ItReturnsCollectionOfHorses()
        {
            // Arrange
            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            _horseService.Verify(mock => mock.GetAll(), Times.Once);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<HorseSummary>>(result.ViewData.Model);
        }
    }
}