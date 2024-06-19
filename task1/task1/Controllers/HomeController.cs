using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using task1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using task1.Services;

namespace task1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IHomeService homeService = new HomeServiceImpl();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new FormModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(FormModel model)
        {
            model = homeService.ParseModel(model);
            return View(model);
        }

    }
    
}
