using Microsoft.AspNetCore.Mvc;
using Project3Vitour.Models;
using Project3Vitour.Services.CategoryServices;
using Project3Vitour.Services.DestinationServices;
using Project3Vitour.Services.ReservationServices;
using Project3Vitour.Services.ReviewServices;
using Project3Vitour.Services.TourImageServices;
using Project3Vitour.Services.TourPlanServices;
using Project3Vitour.Services.TourServices;

namespace Project3Vitour.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ICategoryService _categoryService;
        private readonly IDestinationService _destinationService;
        private readonly ITourPlanService _tourPlanService;
        private readonly ITourImageService _tourImageService;
        private readonly IReviewService _reviewService;
        private readonly IReservationService _reservationService;

        public TourController(
            ITourService tourService,
            ICategoryService categoryService,
            IDestinationService destinationService,
            ITourPlanService tourPlanService,
            ITourImageService tourImageService,
            IReviewService reviewService,
            IReservationService reservationService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
            _destinationService = destinationService;
            _tourPlanService = tourPlanService;
            _tourImageService = tourImageService;
            _reviewService = reviewService;
            _reservationService = reservationService;
        }

        // Sayfalama ve kategori filtresi ViewComponent icinde uygulaniyor;
        // parametreler buradan aktariliyor.
        public IActionResult TourList(int page = 1, string categoryId = null)
        {
            ViewBag.Page = page < 1 ? 1 : page;
            ViewBag.CategoryId = string.IsNullOrWhiteSpace(categoryId) ? null : categoryId;
            return View();
        }

        public async Task<IActionResult> TourDetail(string id)
        {
            var tour = await _tourService.GetTourByIdAsync(id);
            if (tour == null)
                return RedirectToAction("TourList");

            var categories = await _categoryService.GetAllCategoryAsync();
            var destinations = await _destinationService.GetAllDestinationAsync();

            var category = categories.FirstOrDefault(x => x.CategoryId == tour.CategoryId);
            var destination = destinations.FirstOrDefault(x => x.DestinationId == tour.DestinationId);

            var reviews = await _reviewService.GetAllReviewsByTourIdAsync(id);

            var model = new TourDetailViewModel
            {
                Tour = tour,
                CategoryName = category?.CategoryName,
                DestinationName = destination == null ? null : $"{destination.City}, {destination.Country}",
                TourPlans = await _tourPlanService.GetAllTourPlansByTourIdAsync(id),
                TourImages = await _tourImageService.GetAllTourImagesByTourIdAsync(id),
                Reviews = reviews.Where(x => x.Status).ToList(),
                ReservedSeats = await _reservationService.GetReservedSeatCountByTourIdAsync(id),
            };

            return View(model);
        }
    }
}
