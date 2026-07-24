using System.ComponentModel.DataAnnotations;

namespace Project3Vitour.Dtos.ReviewDtos
{
    public class CreateReviewDto
    {
        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "Yorum metni zorunludur.")]
        [StringLength(1000, ErrorMessage = "Yorum metni en fazla 1000 karakter olabilir.")]
        public string Detail { get; set; }

        [Range(1, 5, ErrorMessage = "Puan 1 ile 5 arasında olmalıdır.")]
        public int GuideScore { get; set; }

        [Range(1, 5, ErrorMessage = "Puan 1 ile 5 arasında olmalıdır.")]
        public int AccommodationScore { get; set; }

        [Range(1, 5, ErrorMessage = "Puan 1 ile 5 arasında olmalıdır.")]
        public int TransportScore { get; set; }

        [Range(1, 5, ErrorMessage = "Puan 1 ile 5 arasında olmalıdır.")]
        public int ComfortScore { get; set; }

        // Formdan alinmaz; sunucuda set edilir.
        public DateTime ReviewDate { get; set; }

        // Moderasyon: yorum admin onayina kadar yayinlanmaz.
        public bool Status { get; set; }

        [Required]
        public string TourId { get; set; }
    }
}
