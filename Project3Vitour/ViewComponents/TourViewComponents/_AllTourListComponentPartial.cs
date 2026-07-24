using Microsoft.AspNetCore.Mvc;
using Project3Vitour.Models;
using Project3Vitour.Services.CategoryServices;
using Project3Vitour.Services.DestinationServices;
using Project3Vitour.Services.ReviewServices;
using Project3Vitour.Services.TourServices;

namespace Project3Vitour.ViewComponents.TourViewComponents
{
    public class _AllTourListComponentPartial : ViewComponent
    {
        // Case geregi: her sayfada 6 tur listelenir
        private const int PageSize = 6;

        private readonly ITourService _tourService;
        private readonly ICategoryService _categoryService;
        private readonly IDestinationService _destinationService;
        private readonly IReviewService _reviewService;

        public _AllTourListComponentPartial(
            ITourService tourService,
            ICategoryService categoryService,
            IDestinationService destinationService,
            IReviewService reviewService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
            _destinationService = destinationService;
            _reviewService = reviewService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 1, string categoryId = null)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                categoryId = null;

            var totalCount = await _tourService.GetActiveTourCountAsync(categoryId);

            // Aralik disi sayfa istegi (orn. ?page=999) hem bos liste hem de kayip
            // sayfalama seridi uretiyordu; sayfa numarasi gecerli araliga kirpiliyor.
            var totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
            if (page < 1)
                page = 1;
            if (totalPages > 0 && page > totalPages)
                page = totalPages;

            var tours = await _tourService.GetActiveToursWithPagingAsync(page, PageSize, categoryId);

            var categories = await _categoryService.GetAllCategoryAsync();
            var destinations = await _destinationService.GetAllDestinationAsync();
            var reviews = await _reviewService.GetAllReviewAsync();

            var categoryNames = categories.ToDictionary(x => x.CategoryId, x => x.CategoryName);
            var destinationNames = destinations.ToDictionary(x => x.DestinationId, x => $"{x.City}, {x.Country}");

            // Kartlarda gosterilecek puan/yorum sayisi: sadece yayindaki yorumlar
            var publishedReviews = reviews.Where(x => x.Status).ToList();

            var model = new TourListViewModel
            {
                Tours = tours,
                CurrentPage = page,
                PageSize = PageSize,
                TotalCount = totalCount,
                CategoryId = categoryId,
                CategoryName = categoryId != null && categoryNames.ContainsKey(categoryId)
                    ? categoryNames[categoryId]
                    : null,
            };

            foreach (var tour in tours)
            {
                if (!string.IsNullOrEmpty(tour.CategoryId) && categoryNames.ContainsKey(tour.CategoryId))
                    model.CategoryNames[tour.TourId] = categoryNames[tour.CategoryId];

                if (!string.IsNullOrEmpty(tour.DestinationId) && destinationNames.ContainsKey(tour.DestinationId))
                    model.DestinationNames[tour.TourId] = destinationNames[tour.DestinationId];

                var tourReviews = publishedReviews.Where(x => x.TourId == tour.TourId).ToList();
                model.ReviewCounts[tour.TourId] = tourReviews.Count;
                model.AverageScores[tour.TourId] = tourReviews.Any()
                    ? tourReviews.Average(x => x.AverageScore)
                    : 0;
            }

            return View(model);
        }
    }
}
