using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Project3Vitour.Controllers
{
    public class LanguageController : Controller
    {
        // Dil secimini cookie'ye yazar ve kullaniciyi geldigi sayfaya geri dondurur.
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true
                });

            // Acik yonlendirme (open redirect) riskini onlemek icin sadece site ici adresler
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("TourList", "Tour");
        }
    }
}
