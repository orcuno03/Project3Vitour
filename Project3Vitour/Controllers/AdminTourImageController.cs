using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project3Vitour.Dtos.TourImageDtos;
using Project3Vitour.Services.TourImageServices;
using Project3Vitour.Services.TourServices;

namespace Project3Vitour.Controllers
{
    public class AdminTourImageController : Controller
    {
        private readonly ITourImageService _tourImageService;
        private readonly ITourService _tourService;

        public AdminTourImageController(ITourImageService tourImageService, ITourService tourService)
        {
            _tourImageService = tourImageService;
            _tourService = tourService;
        }

        public async Task<IActionResult> TourImageList()
        {
            await LoadToursAsync();
            var values = await _tourImageService.GetAllTourImageAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTourImage()
        {
            await LoadToursAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTourImage(CreateTourImageDto createTourImageDto)
        {
            await _tourImageService.CreateTourImageAsync(createTourImageDto);
            return RedirectToAction("TourImageList");
        }

        public async Task<IActionResult> DeleteTourImage(string id)
        {
            await _tourImageService.DeleteTourImageAsync(id);
            return RedirectToAction("TourImageList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTourImage(string id)
        {
            await LoadToursAsync();
            var value = await _tourImageService.GetTourImageByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTourImage(UpdateTourImageDto updateTourImageDto)
        {
            await _tourImageService.UpdateTourImageAsync(updateTourImageDto);
            return RedirectToAction("TourImageList");
        }

        private async Task LoadToursAsync()
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = new SelectList(tours, "TourId", "Title");
            ViewBag.TourTitles = tours.ToDictionary(x => x.TourId, x => x.Title);
        }
    }
}
