using Project3Vitour.Dtos.ReviewDtos;
using Project3Vitour.Dtos.TourDtos;
using Project3Vitour.Dtos.TourImageDtos;
using Project3Vitour.Dtos.TourPlanDtos;

namespace Project3Vitour.Models
{
    // Tur detay sayfasi: 5 sekmenin (Bilgiler / Program / Konum / Yorumlar / Galeri) tum verisi
    public class TourDetailViewModel
    {
        public GetTourByIdDto Tour { get; set; }

        public string CategoryName { get; set; }
        public string DestinationName { get; set; }

        public List<ResultTourPlanByTourIdDto> TourPlans { get; set; } = new();
        public List<ResultTourImageByTourIdDto> TourImages { get; set; } = new();

        // Sadece yayinlanmis (onaylanmis) yorumlar
        public List<ResultReviewByTourIdDto> Reviews { get; set; } = new();

        // Kontenjan durumu
        public int ReservedSeats { get; set; }
        public int RemainingSeats => Math.Max(0, (Tour?.Capacity ?? 0) - ReservedSeats);
        public bool IsFull => RemainingSeats <= 0;

        public double AverageScore => Reviews.Any() ? Reviews.Average(x => x.AverageScore) : 0;
        public double AverageGuideScore => Reviews.Any() ? Reviews.Average(x => x.GuideScore) : 0;
        public double AverageAccommodationScore => Reviews.Any() ? Reviews.Average(x => x.AccommodationScore) : 0;
        public double AverageTransportScore => Reviews.Any() ? Reviews.Average(x => x.TransportScore) : 0;
        public double AverageComfortScore => Reviews.Any() ? Reviews.Average(x => x.ComfortScore) : 0;
    }
}
