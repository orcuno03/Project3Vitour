using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Project3Vitour.Services.CategoryServices;
using Project3Vitour.Services.DestinationServices;
using Project3Vitour.Services.ExportServices;
using Project3Vitour.Services.ReservationServices;
using Project3Vitour.Services.ReviewServices;
using Project3Vitour.Services.TourImageServices;
using Project3Vitour.Services.TourPlanServices;
using Project3Vitour.Services.TourServices;
using Project3Vitour.Settings;

var builder = WebApplication.CreateBuilder(args);

// QuestPDF ucretsiz Community lisansi (rapor/PDF ciktisi icin gerekli)
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IDestinationService, DestinationService>();
builder.Services.AddScoped<ITourPlanService, TourPlanService>();
builder.Services.AddScoped<ITourImageService, TourImageService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationExportService, ReservationExportService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingKey"));

builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
}); 

// Coklu dil (TR/EN): ceviriler Resources/SharedResource.{tr,en}.resx dosyalarindan okunur.
// ResourcesPath BILEREK ayarlanmadi: SharedResource.cs ile resx dosyalari ayni
// klasorde oldugu icin SDK (EmbeddedResourceUseDependentUponConvention) kaynak adini
// klasor yolundan degil sinifin namespace'inden turetiyor => "Project3Vitour.SharedResource".
// ResourcesPath="Resources" verilseydi localizer "Project3Vitour.Resources.SharedResource"
// arar ve ceviriler bulunamazdi.
builder.Services.AddLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("tr"),
        new CultureInfo("en")
    };

    options.DefaultRequestCulture = new RequestCulture("tr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    // Dil secimi cookie tabanli yonetilir: sadece cookie'ye bakilir,
    // tarayici Accept-Language basligi dikkate alinmaz.
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new CookieRequestCultureProvider()
    };
});

builder.Services.AddControllersWithViews()
                .AddViewLocalization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
