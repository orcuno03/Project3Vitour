using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project3Vitour.Dtos.TourPlanDtos;
using Project3Vitour.Services.TourPlanServices;
using Project3Vitour.Services.TourServices;

namespace Project3Vitour.Controllers
{
    public class AdminTourPlanController : Controller
    {
        private readonly ITourPlanService _tourPlanService;
        private readonly ITourService _tourService;

        public AdminTourPlanController(ITourPlanService tourPlanService, ITourService tourService)
        {
            _tourPlanService = tourPlanService;
            _tourService = tourService;
        }

        public async Task<IActionResult> TourPlanList()
        {
            await LoadToursAsync();
            var values = await _tourPlanService.GetAllTourPlanAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTourPlan()
        {
            await LoadToursAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTourPlan(CreateTourPlanDto createTourPlanDto)
        {
            await _tourPlanService.CreateTourPlanAsync(createTourPlanDto);
            return RedirectToAction("TourPlanList");
        }

        public async Task<IActionResult> DeleteTourPlan(string id)
        {
            await _tourPlanService.DeleteTourPlanAsync(id);
            return RedirectToAction("TourPlanList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTourPlan(string id)
        {
            await LoadToursAsync();
            var value = await _tourPlanService.GetTourPlanByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTourPlan(UpdateTourPlanDto updateTourPlanDto)
        {
            await _tourPlanService.UpdateTourPlanAsync(updateTourPlanDto);
            return RedirectToAction("TourPlanList");
        }

        // Tur secim listesi + listeleme ekraninda TourId -> Baslik cozumlemesi icin
        private async Task LoadToursAsync()
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = new SelectList(tours, "TourId", "Title");
            ViewBag.TourTitles = tours.ToDictionary(x => x.TourId, x => x.Title);
        }
    }
}
