using Microsoft.AspNetCore.Mvc;
using Project3Vitour.Services.CategoryServices;

namespace Project3Vitour.ViewComponents.TourViewComponents
{
    public class _TourHeaderComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _TourHeaderComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var kategoriler = await _categoryService.GetAllCategoryAsync();
            return View(kategoriler.Where(x => x.CategoryStatus).ToList());
        }
    }
}
