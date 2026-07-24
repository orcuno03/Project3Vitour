using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Project3Vitour.Dtos.ReviewDtos;
using Project3Vitour.Services.ReviewServices;

namespace Project3Vitour.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ReviewController(IReviewService reviewService, IStringLocalizer<SharedResource> localizer)
        {
            _reviewService = reviewService;
            _localizer = localizer;
        }

        // Form, tur detayindaki Yorumlar sekmesinde gomulu; bu yuzden ayri bir GET sayfasi yok.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview(CreateReviewDto createReviewDto)
        {
            if (string.IsNullOrEmpty(createReviewDto.TourId))
            {
                return RedirectToAction("TourList", "Tour");
            }

            if (!ModelState.IsValid)
            {
                TempData["ReviewError"] = _localizer["Review_Invalid"].Value;
                return Redirect(Url.Action("TourDetail", "Tour", new { id = createReviewDto.TourId }) + "#reviews");
            }

            createReviewDto.ReviewDate = DateTime.Now;
            createReviewDto.Status = false;

            await _reviewService.CreateReviewAsync(createReviewDto);

            TempData["ReviewMessage"] = _localizer["Review_Received"].Value;
            return Redirect(Url.Action("TourDetail", "Tour", new { id = createReviewDto.TourId }) + "#reviews");
        }
    }
}
