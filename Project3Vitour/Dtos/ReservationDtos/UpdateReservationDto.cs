using System.ComponentModel.DataAnnotations;

namespace Project3Vitour.Dtos.ReservationDtos
{
    public class UpdateReservationDto
    {
        public string ReservationId { get; set; }

        [Required(ErrorMessage = "Ad soyad alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        public string Phone { get; set; }

        [Range(1, 50, ErrorMessage = "Kişi sayısı 1 ile 50 arasında olmalıdır.")]
        public int PersonCount { get; set; }

        [Required(ErrorMessage = "Rezervasyon tarihi zorunludur.")]
        public DateTime ReservationDate { get; set; }

        // Istege bagli alan: bos birakildiginda dogrulamayi tetiklememesi icin nullable
        public string? Note { get; set; }

        [Required(ErrorMessage = "Rezervasyon durumu seçilmelidir.")]
        public string ReservationStatus { get; set; }

        [Required(ErrorMessage = "Tur seçilmelidir.")]
        public string TourId { get; set; }
    }
}
