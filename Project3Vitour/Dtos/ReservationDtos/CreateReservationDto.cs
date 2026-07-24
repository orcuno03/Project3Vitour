using System.ComponentModel.DataAnnotations;

namespace Project3Vitour.Dtos.ReservationDtos
{
    public class CreateReservationDto
    {
        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Phone { get; set; }

        [Range(1, 50, ErrorMessage = "Kişi sayısı 1 ile 50 arasında olmalıdır.")]
        public int PersonCount { get; set; }

        // Nullable tutuluyor: alan bos gelirse tip donusum hatasi yerine Turkce Required mesaji cikar.
        [Required(ErrorMessage = "Rezervasyon tarihi zorunludur.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReservationDate { get; set; }

        [StringLength(500, ErrorMessage = "Not en fazla 500 karakter olabilir.")]
        public string? Note { get; set; }

        // Formdan gelmez, sunucu tarafinda doldurulur.
        public string? ReservationStatus { get; set; }

        public string TourId { get; set; }
    }
}
