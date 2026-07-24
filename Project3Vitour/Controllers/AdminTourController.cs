using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project3Vitour.Dtos.TourDtos;
using Project3Vitour.Services.CategoryServices;
using Project3Vitour.Services.DestinationServices;
using Project3Vitour.Services.TourServices;

namespace Project3Vitour.Controllers
{
    public class AdminTourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ICategoryService _categoryService;
        private readonly IDestinationService _destinationService;

        public AdminTourController(ITourService tourService, ICategoryService categoryService, IDestinationService destinationService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
            _destinationService = destinationService;
        }

        public async Task<IActionResult> TourList()
        {
            await LoadLookupsAsync();
            var values = await _tourService.GetAllTourAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTour()
        {
            await LoadLookupsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTour(CreateTourDto createTourDto)
        {
            if (!ModelState.IsValid)
            {
                await LoadLookupsAsync();
                return View(createTourDto);
            }

            await _tourService.CreateTourAsync(createTourDto);
            return RedirectToAction("TourList");
        }

        public async Task<IActionResult> DeleteTour(string id)
        {
            await _tourService.DeleteTourAsync(id);
            return RedirectToAction("TourList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTour(string id)
        {
            var value = await _tourService.GetTourByIdAsync(id);
            if (value == null)
            {
                return RedirectToAction("TourList");
            }

            await LoadLookupsAsync();

            // Form dogrudan UpdateTourDto uzerinden calisir; hatali gonderimde ayni model geri donulur
            var updateTourDto = new UpdateTourDto
            {
                TourId = value.TourId,
                Title = value.Title,
                Description = value.Description,
                CoverImageUrl = value.CoverImageUrl,
                MapLocationImageUrl = value.MapLocationImageUrl,
                Badge = value.Badge,
                DayCount = value.DayCount,
                Capacity = value.Capacity,
                Price = value.Price,
                IsStatus = value.IsStatus,
                CategoryId = value.CategoryId,
                DestinationId = value.DestinationId
            };

            return View(updateTourDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTour(UpdateTourDto updateTourDto)
        {
            if (!ModelState.IsValid)
            {
                await LoadLookupsAsync();
                return View(updateTourDto);
            }

            await _tourService.UpdateTourAsync(updateTourDto);
            return RedirectToAction("TourList");
        }

        // Kategori/Lokasyon secim listeleri + listeleme ekraninda Id -> Ad cozumlemesi
        private async Task LoadLookupsAsync()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            var destinations = await _destinationService.GetAllDestinationAsync();

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Destinations = new SelectList(destinations, "DestinationId", "City");

            ViewBag.CategoryNames = categories.ToDictionary(x => x.CategoryId, x => x.CategoryName);
            ViewBag.DestinationNames = destinations.ToDictionary(x => x.DestinationId, x => x.City);
        }
    }
}
