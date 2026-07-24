using Microsoft.AspNetCore.Mvc;
using Project3Vitour.Entities;
using Project3Vitour.Services.CategoryServices;
using Project3Vitour.Services.DestinationServices;
using Project3Vitour.Services.ReservationServices;
using Project3Vitour.Services.ReviewServices;
using Project3Vitour.Services.TourServices;

namespace Project3Vitour.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IReservationService _reservationService;
        private readonly IReviewService _reviewService;
        private readonly ICategoryService _categoryService;
        private readonly IDestinationService _destinationService;

        public AdminDashboardController(
            ITourService tourService,
            IReservationService reservationService,
            IReviewService reviewService,
            ICategoryService categoryService,
            IDestinationService destinationService)
        {
            _tourService = tourService;
            _reservationService = reservationService;
            _reviewService = reviewService;
            _categoryService = categoryService;
            _destinationService = destinationService;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllTourAsync();
            var reservations = await _reservationService.GetAllReservationAsync();
            var reviews = await _reviewService.GetAllReviewAsync();
            var categories = await _categoryService.GetAllCategoryAsync();
            var destinations = await _destinationService.GetAllDestinationAsync();

            var tourDict = tours.ToDictionary(x => x.TourId, x => x);

            // Calculation metrics
            var totalTourCount = tours.Count;
            var totalReservationCount = reservations.Count;
            var approvedReservationCount = reservations.Count(r => r.ReservationStatus == ReservationStatuses.Approved);
            var pendingReservationCount = reservations.Count(r => r.ReservationStatus == ReservationStatuses.Pending);
            var cancelledReservationCount = reservations.Count(r => r.ReservationStatus == ReservationStatuses.Cancelled);

            // Calculate total revenue from approved reservations
            decimal totalRevenue = 0;
            foreach (var res in reservations.Where(r => r.ReservationStatus == ReservationStatuses.Approved))
            {
                if (tourDict.TryGetValue(res.TourId, out var tour))
                {
                    totalRevenue += tour.Price * res.PersonCount;
                }
            }

            var avgReviewScore = reviews.Any() ? reviews.Average(r => (r.GuideScore + r.AccommodationScore + r.TransportScore + r.ComfortScore) / 4.0) : 0;

            // Most popular tour based on reservation count
            var popularTourGroup = reservations
                .GroupBy(r => r.TourId)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            string popularTourTitle = "Henüz Yok";
            if (popularTourGroup != null && tourDict.TryGetValue(popularTourGroup.Key, out var popTour))
            {
                popularTourTitle = popTour.Title;
            }

            ViewBag.TotalTourCount = totalTourCount;
            ViewBag.TotalReservationCount = totalReservationCount;
            ViewBag.ApprovedReservationCount = approvedReservationCount;
            ViewBag.PendingReservationCount = pendingReservationCount;
            ViewBag.CancelledReservationCount = cancelledReservationCount;
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.TotalCategoryCount = categories.Count;
            ViewBag.TotalDestinationCount = destinations.Count;
            ViewBag.TotalReviewCount = reviews.Count;
            ViewBag.AvgReviewScore = avgReviewScore;
            ViewBag.PopularTourTitle = popularTourTitle;
            ViewBag.TourDict = tours.ToDictionary(x => x.TourId, x => x.Title);

            ViewBag.RecentReservations = reservations.OrderByDescending(r => r.ReservationDate).Take(6).ToList();
            ViewBag.RecentReviews = reviews.OrderByDescending(r => r.ReviewDate).Take(5).ToList();

            return View();
        }
    }
}
