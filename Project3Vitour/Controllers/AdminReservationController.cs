using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project3Vitour.Dtos.ReservationDtos;
using Project3Vitour.Entities;
using Project3Vitour.Services.ExportServices;
using Project3Vitour.Services.ReservationServices;
using Project3Vitour.Services.TourServices;

namespace Project3Vitour.Controllers
{
    public class AdminReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITourService _tourService;
        private readonly IReservationExportService _exportService;

        public AdminReservationController(
            IReservationService reservationService,
            ITourService tourService,
            IReservationExportService exportService)
        {
            _reservationService = reservationService;
            _tourService = tourService;
            _exportService = exportService;
        }

        public async Task<IActionResult> ReservationList()
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.TourTitles = tours.ToDictionary(x => x.TourId, x => x.Title);

            var values = await _reservationService.GetAllReservationAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateReservation()
        {
            await LoadToursAsync();

            return View(new CreateReservationDto
            {
                PersonCount = 1,
                ReservationDate = DateTime.Now,
                ReservationStatus = ReservationStatuses.Pending
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            ValidateCreateFields(createReservationDto);

            if (ModelState.IsValid)
            {
                var tour = await _tourService.GetTourByIdAsync(createReservationDto.TourId);
                if (tour == null)
                {
                    ModelState.AddModelError(nameof(createReservationDto.TourId), "Seçilen tur bulunamadı.");
                }
                else
                {
                    // Kontenjan kontrolu: kapasite asiliyorsa kayit atilmaz
                    var reservedSeats = await _reservationService.GetReservedSeatCountByTourIdAsync(tour.TourId);
                    var remainingSeats = Math.Max(0, tour.Capacity - reservedSeats);

                    if (createReservationDto.PersonCount > remainingSeats)
                    {
                        ModelState.AddModelError(nameof(createReservationDto.PersonCount),
                            $"Bu tur için yeterli kontenjan yok. Kalan yer: {remainingSeats}");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                await LoadToursAsync();
                return View(createReservationDto);
            }

            if (string.IsNullOrWhiteSpace(createReservationDto.ReservationStatus))
                createReservationDto.ReservationStatus = ReservationStatuses.Pending;

            await _reservationService.CreateReservationAsync(createReservationDto);

            return RedirectToAction("ReservationList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateReservation(string id)
        {
            var value = await _reservationService.GetReservationByIdAsync(id);
            if (value == null)
                return RedirectToAction("ReservationList");

            await LoadToursAsync();
            LoadStatuses(value.ReservationStatus);

            return View(new UpdateReservationDto
            {
                ReservationId = value.ReservationId,
                NameSurname = value.NameSurname,
                Email = value.Email,
                Phone = value.Phone,
                PersonCount = value.PersonCount,
                ReservationDate = value.ReservationDate,
                Note = value.Note,
                ReservationStatus = value.ReservationStatus,
                TourId = value.TourId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            if (!ModelState.IsValid)
            {
                await LoadToursAsync();
                LoadStatuses(updateReservationDto.ReservationStatus);
                return View(updateReservationDto);
            }

            await _reservationService.UpdateReservationAsync(updateReservationDto);

            return RedirectToAction("ReservationList");
        }

        public async Task<IActionResult> ChangeReservationStatus(string id, string status)
        {
            var value = await _reservationService.GetReservationByIdAsync(id);
            if (value == null)
                return RedirectToAction("ReservationList");

            await _reservationService.UpdateReservationAsync(new UpdateReservationDto
            {
                ReservationId = value.ReservationId,
                NameSurname = value.NameSurname,
                Email = value.Email,
                Phone = value.Phone,
                PersonCount = value.PersonCount,
                ReservationDate = value.ReservationDate,
                Note = value.Note,
                ReservationStatus = status,
                TourId = value.TourId
            });

            return RedirectToAction("ReservationList");
        }

        public async Task<IActionResult> ApproveReservation(string id)
            => await ChangeReservationStatus(id, ReservationStatuses.Approved);

        public async Task<IActionResult> CancelReservation(string id)
            => await ChangeReservationStatus(id, ReservationStatuses.Cancelled);

        public async Task<IActionResult> DeleteReservation(string id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction("ReservationList");
        }

        // Ilgili tura kayit olan kullanicilarin listesini Excel olarak uretir
        public async Task<IActionResult> ExportToExcel(string id)
        {
            var (tourTitle, reservations) = await GetTourReservationsAsync(id);

            var content = _exportService.GenerateExcel(tourTitle, reservations);

            return File(content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"{FileNameFor(tourTitle)}.xlsx");
        }

        // Ilgili tura kayit olan kullanicilarin listesini PDF olarak uretir
        public async Task<IActionResult> ExportToPdf(string id)
        {
            var (tourTitle, reservations) = await GetTourReservationsAsync(id);

            var content = _exportService.GeneratePdf(tourTitle, reservations);

            return File(content, "application/pdf", $"{FileNameFor(tourTitle)}.pdf");
        }

        // CreateReservationDto'da DataAnnotations yok; zorunlu alan uyarilari Turkce olsun diye elle kuruluyor
        private void ValidateCreateFields(CreateReservationDto dto)
        {
            SetRequiredError(nameof(dto.TourId), dto.TourId, "Tur seçilmelidir.");
            SetRequiredError(nameof(dto.NameSurname), dto.NameSurname, "Ad soyad alanı zorunludur.");
            SetRequiredError(nameof(dto.Email), dto.Email, "E-posta alanı zorunludur.");
            SetRequiredError(nameof(dto.Phone), dto.Phone, "Telefon alanı zorunludur.");

            if (dto.PersonCount < 1 || dto.PersonCount > 50)
            {
                ModelState.Remove(nameof(dto.PersonCount));
                ModelState.AddModelError(nameof(dto.PersonCount), "Kişi sayısı 1 ile 50 arasında olmalıdır.");
            }
        }

        private void SetRequiredError(string key, string? value, string message)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return;

            ModelState.Remove(key);
            ModelState.AddModelError(key, message);
        }

        private async Task LoadToursAsync()
        {
            var tours = await _tourService.GetAllTourAsync();
            ViewBag.Tours = new SelectList(tours, "TourId", "Title");
        }

        private void LoadStatuses(string selected)
        {
            var statuses = new[]
            {
                ReservationStatuses.Pending,
                ReservationStatuses.Approved,
                ReservationStatuses.Cancelled
            };

            ViewBag.Statuses = new SelectList(statuses, selected);
        }

        private async Task<(string TourTitle, List<ResultReservationByTourIdDto> Reservations)> GetTourReservationsAsync(string tourId)
        {
            var tour = await _tourService.GetTourByIdAsync(tourId);
            var reservations = await _reservationService.GetAllReservationsByTourIdAsync(tourId);

            return (tour?.Title ?? "Bilinmeyen Tur", reservations);
        }

        // Dosya adinda kullanilamayan karakterleri temizler
        private static string FileNameFor(string tourTitle)
        {
            var name = string.Concat(tourTitle.Split(Path.GetInvalidFileNameChars()));
            return $"{name}-Rezervasyonlar-{DateTime.Now:yyyyMMdd}";
        }
    }
}
