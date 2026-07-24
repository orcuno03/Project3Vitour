using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project3Vitour.Models;

namespace Project3Vitour.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Sitenin giris noktasi tur listesidir.
        public IActionResult Index()
        {
            return RedirectToAction("TourList", "Tour");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
