using System;
using Example.Services.Interfaces;
using Example.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Example.Web.Controllers
{
    public class HorsesController : Controller
    {
        private readonly IHorseService _horseService;
        private readonly IMapper<Dto.Horse, Models.HorseSummary> _horseSummaryMapper;
        private readonly IMapper<Dto.Horse, Models.HorseDetail> _horseDetailMapper;

        public HorsesController(IHorseService horseService,
            IMapper<Dto.Horse, Models.HorseSummary> horseSummaryMapper,
            IMapper<Dto.Horse, Models.HorseDetail> horseDetailMapper)
        {
            _horseService = horseService;
            _horseSummaryMapper = horseSummaryMapper;
            _horseDetailMapper = horseDetailMapper;
        }

        public ActionResult Index()
        {
            Console.WriteLine($"Getting all horses - {DateTime.UtcNow}");

            var horses = _horseService.GetAll();

            Console.WriteLine($"Mapping all horses - {DateTime.UtcNow}");

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
