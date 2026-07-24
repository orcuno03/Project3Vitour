using System.ComponentModel.DataAnnotations;

namespace Project3Vitour.Dtos.ReviewDtos
{
    public class UpdateReviewDto
    {
        public string ReviewId { get; set; }

        [Required(ErrorMessage = "Ad soyad alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "Yorum metni zorunludur.")]
        [StringLength(1000, ErrorMessage = "Yorum en fazla 1000 karakter olabilir.")]
        public string Detail { get; set; }

        [Range(1, 5, ErrorMessage = "Rehber puanı 1 ile 5 arasında olmalıdır.")]
        public int GuideScore { get; set; }

        [Range(1, 5, ErrorMessage = "Konaklama puanı 1 ile 5 arasında olmalıdır.")]
        public int AccommodationScore { get; set; }

        [Range(1, 5, ErrorMessage = "Ulaşım puanı 1 ile 5 arasında olmalıdır.")]
        public int TransportScore { get; set; }

        [Range(1, 5, ErrorMessage = "Konfor puanı 1 ile 5 arasında olmalıdır.")]
        public int ComfortScore { get; set; }

        public DateTime ReviewDate { get; set; }
        public bool Status { get; set; }
        public string TourId { get; set; }
    }
}
