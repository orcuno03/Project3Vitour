using Microsoft.AspNetCore.Mvc;
using Project3Vitour.Dtos.ReservationDtos;
using Project3Vitour.Dtos.TourDtos;
using Project3Vitour.Entities;
using Project3Vitour.Services.MailServices;
using Project3Vitour.Services.ReservationServices;
using Project3Vitour.Services.TourServices;

namespace Project3Vitour.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITourService _tourService;
        private readonly IMailService _mailService;

        public ReservationController(
            IReservationService reservationService,
            ITourService tourService,
            IMailService mailService)
        {
            _reservationService = reservationService;
            _tourService = tourService;
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateReservation(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return RedirectToAction("TourList", "Tour");

            var tour = await _tourService.GetTourByIdAsync(id);

            // Yayindan kaldirilmis tura rezervasyon alinmaz
            if (tour == null || !tour.IsStatus)
                return RedirectToAction("TourList", "Tour");

            await LoadTourContextAsync(tour);

            return View(new CreateReservationDto
            {
                TourId = tour.TourId,
                PersonCount = 1,
                ReservationDate = DateTime.Today
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            if (string.IsNullOrWhiteSpace(createReservationDto.TourId))
                return RedirectToAction("TourList", "Tour");

            var tour = await _tourService.GetTourByIdAsync(createReservationDto.TourId);

            if (tour == null || !tour.IsStatus)
                return RedirectToAction("TourList", "Tour");

            var reservedSeats = await _reservationService.GetReservedSeatCountByTourIdAsync(tour.TourId);
            var remainingSeats = Math.Max(0, tour.Capacity - reservedSeats);

            // Kontenjan kontrolu: tur dolu olabilir ya da istenen kisi sayisi kalan yeri asabilir
            if (remainingSeats <= 0)
            {
                ModelState.AddModelError(string.Empty,
                    "Bu turun kontenjanı dolmuştur, yeni rezervasyon alınamıyor.");
            }
            else if (createReservationDto.PersonCount > remainingSeats)
            {
                ModelState.AddModelError(nameof(createReservationDto.PersonCount),
                    $"Yeterli kontenjan yok. Kalan yer: {remainingSeats} kişi.");
            }

            if (createReservationDto.ReservationDate.HasValue &&
                createReservationDto.ReservationDate.Value.Date < DateTime.Today)
            {
                ModelState.AddModelError(nameof(createReservationDto.ReservationDate),
                    "Geçmiş bir tarih için rezervasyon oluşturulamaz.");
            }

            if (!ModelState.IsValid)
            {
                await LoadTourContextAsync(tour);
                return View(createReservationDto);
            }

            createReservationDto.ReservationStatus = ReservationStatuses.Pending;

            await _reservationService.CreateReservationAsync(createReservationDto);

            // E-posta bildirimi gönder (Arka planda asenkron olarak)
            var totalPrice = tour.Price * createReservationDto.PersonCount;
            _ = _mailService.SendReservationConfirmationEmailAsync(
                createReservationDto.Email,
                createReservationDto.NameSurname,
                tour.Title,
                createReservationDto.ReservationDate ?? DateTime.Now,
                createReservationDto.PersonCount,
                totalPrice,
                createReservationDto.ReservationStatus);

            TempData["TourTitle"] = tour.Title;
            TempData["PersonCount"] = createReservationDto.PersonCount;

            return RedirectToAction("ReservationSuccess");
        }

        public IActionResult ReservationSuccess()
        {
            ViewBag.TourTitle = TempData["TourTitle"] as string;
            ViewBag.PersonCount = TempData["PersonCount"] as int?;

            if (ViewBag.TourTitle == null)
                return RedirectToAction("TourList", "Tour");

            return View();
        }

        // Rezervasyon formunda gosterilecek tur bilgisi ve kalan kontenjan
        private async Task LoadTourContextAsync(GetTourByIdDto tour)
        {
            var reservedSeats = await _reservationService.GetReservedSeatCountByTourIdAsync(tour.TourId);
            var remainingSeats = Math.Max(0, tour.Capacity - reservedSeats);

            ViewBag.Tour = tour;
            ViewBag.RemainingSeats = remainingSeats;
            ViewBag.IsFull = remainingSeats <= 0;
        }
    }
}
