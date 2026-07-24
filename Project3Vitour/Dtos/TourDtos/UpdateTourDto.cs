using System.ComponentModel.DataAnnotations;

namespace Project3Vitour.Dtos.TourDtos
{
    public class UpdateTourDto
    {
        public string TourId { get; set; }

        [Required(ErrorMessage = "Tur adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Tur adı en fazla 100 karakter olabilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tur açıklaması zorunludur.")]
        [StringLength(500, ErrorMessage = "Tur açıklaması en fazla 500 karakter olabilir.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Kapak görseli zorunludur.")]
        public string CoverImageUrl { get; set; }

        public string MapLocationImageUrl { get; set; }

        public string Badge { get; set; }

        [Range(1, 365, ErrorMessage = "Tur süresi 1 ile 365 gün arasında olmalıdır.")]
        public int DayCount { get; set; }

        [Range(1, 999, ErrorMessage = "Kapasite 1 ile 999 kişi arasında olmalıdır.")]
        public int Capacity { get; set; }

        [Range(typeof(decimal), "1", "1000000", ErrorMessage = "Fiyat 1 ile 1000000 arasında olmalıdır.")]
        public decimal Price { get; set; }

        public bool IsStatus { get; set; }

        public string CategoryId { get; set; }

        public string DestinationId { get; set; }
    }
}
