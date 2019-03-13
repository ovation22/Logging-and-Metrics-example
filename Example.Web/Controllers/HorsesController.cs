using System;
using Example.Services.Interfaces;
using Example.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Example.Web.Controllers
{
    public class HorsesController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHorseService _horseService;
        private readonly IMapper<Dto.Horse, Models.HorseSummary> _horseSummaryMapper;
        private readonly IMapper<Dto.Horse, Models.HorseDetail> _horseDetailMapper;

        public HorsesController(
            IHorseService horseService,
            ILogger<HorsesController> logger,
            IMapper<Dto.Horse, Models.HorseDetail> horseDetailMapper,
            IMapper<Dto.Horse, Models.HorseSummary> horseSummaryMapper
        )
        {
            _logger = logger;
            _horseService = horseService;
            _horseDetailMapper = horseDetailMapper;
            _horseSummaryMapper = horseSummaryMapper;
        }

        public ActionResult Index()
        {
            _logger.LogInformation($"Getting all horses - {DateTime.UtcNow}");

            var horses = _horseService.GetAll();

            _logger.LogInformation($"Mapping all horses - {DateTime.UtcNow}");

            var model = horses.Select(_horseSummaryMapper.Map).ToList();

            ViewBag.Message = "Default";

            return View("Index", model);
        }

        public ActionResult Detail(int id)
        {
            var horse = _horseService.Get(id);

            var model = _horseDetailMapper.Map(horse);

            ViewBag.Message = "Default";

            return View("Detail", model);
        }        
    }
}
