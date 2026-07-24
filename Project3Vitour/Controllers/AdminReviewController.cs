using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project3Vitour.Dtos.ReviewDtos;
using Project3Vitour.Services.ReviewServices;
using Project3Vitour.Services.TourServices;

namespace Project3Vitour.Controllers
{
    public class AdminReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ITourService _tourService;

        public AdminReviewController(IReviewService reviewService, ITourService tourService)
        {
            _reviewService = reviewService;
            _tourService = tourService;
        }

        public async Task<IActionResult> ReviewList()
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.TourTitles = tours.ToDictionary(x => x.TourId, x => x.Title);

            var values = await _reviewService.GetAllReviewAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateReview()
        {
            await LoadToursAsync();
            return View(new CreateReviewDto
            {
                ReviewDate = DateTime.Now,
                GuideScore = 5,
                AccommodationScore = 5,
                TransportScore = 5,
                ComfortScore = 5,
                Status = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview(CreateReviewDto createReviewDto)
        {
            if (!ModelState.IsValid)
            {
                await LoadToursAsync();
                return View(createReviewDto);
            }

            await _reviewService.CreateReviewAsync(createReviewDto);
            return RedirectToAction("ReviewList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateReview(string id)
        {
            var value = await _reviewService.GetReviewByIdAsync(id);
            if (value == null)
                return RedirectToAction("ReviewList");

            await LoadToursAsync();

            return View(new UpdateReviewDto
            {
                ReviewId = value.ReviewId,
                NameSurname = value.NameSurname,
                Detail = value.Detail,
                GuideScore = value.GuideScore,
                AccommodationScore = value.AccommodationScore,
                TransportScore = value.TransportScore,
                ComfortScore = value.ComfortScore,
                ReviewDate = value.ReviewDate,
                Status = value.Status,
                TourId = value.TourId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto updateReviewDto)
        {
            if (!ModelState.IsValid)
            {
                await LoadToursAsync();
                return View(updateReviewDto);
            }

            await _reviewService.UpdateReviewAsync(updateReviewDto);
            return RedirectToAction("ReviewList");
        }

        // Yorumu yayina al / yayindan kaldir (Status alanini tersine cevirir)
        public async Task<IActionResult> ChangeReviewStatus(string id)
        {
            var value = await _reviewService.GetReviewByIdAsync(id);
            if (value == null)
                return RedirectToAction("ReviewList");

            await _reviewService.UpdateReviewAsync(new UpdateReviewDto
            {
                ReviewId = value.ReviewId,
                NameSurname = value.NameSurname,
                Detail = value.Detail,
                GuideScore = value.GuideScore,
                AccommodationScore = value.AccommodationScore,
                TransportScore = value.TransportScore,
                ComfortScore = value.ComfortScore,
                ReviewDate = value.ReviewDate,
                Status = !value.Status,
                TourId = value.TourId
            });

            return RedirectToAction("ReviewList");
        }

        public async Task<IActionResult> DeleteReview(string id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return RedirectToAction("ReviewList");
        }

        // Form ekranlarindaki tur secim listesi
        private async Task LoadToursAsync()
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = new SelectList(tours, "TourId", "Title");
        }
    }
}
