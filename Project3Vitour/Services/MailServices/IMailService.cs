namespace Project3Vitour.Services.MailServices
{
    public interface IMailService
    {
        Task SendReservationConfirmationEmailAsync(
            string toEmail,
            string nameSurname,
            string tourTitle,
            DateTime reservationDate,
            int personCount,
            decimal totalPrice,
            string reservationStatus);

        Task SendStatusUpdateEmailAsync(
            string toEmail,
            string nameSurname,
            string tourTitle,
            string newStatus);
    }
}
