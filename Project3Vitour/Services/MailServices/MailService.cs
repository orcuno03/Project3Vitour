using System.Net;
using System.Net.Mail;

namespace Project3Vitour.Services.MailServices
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailService> _logger;

        public MailService(IConfiguration configuration, ILogger<MailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendReservationConfirmationEmailAsync(
            string toEmail,
            string nameSurname,
            string tourTitle,
            DateTime reservationDate,
            int personCount,
            decimal totalPrice,
            string reservationStatus)
        {
            var subject = $"Vitour - Rezervasyon Talebiniz Alındı: {tourTitle}";
            var body = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 8px; overflow: hidden;'>
                    <div style='background-color: #1a7ab5; color: white; padding: 20px; text-align: center;'>
                        <h1 style='margin: 0; font-size: 24px;'>Vitour Turizm</h1>
                        <p style='margin: 5px 0 0; font-size: 14px;'>Rezervasyon Onayı</p>
                    </div>
                    <div style='padding: 24px; color: #333;'>
                        <p>Sayın <strong>{WebUtility.HtmlEncode(nameSurname)}</strong>,</p>
                        <p><strong>{WebUtility.HtmlEncode(tourTitle)}</strong> için rezervasyon talebiniz başarıyla alınmıştır.</p>
                        
                        <table style='width: 100%; border-collapse: collapse; margin: 20px 0;'>
                            <tr style='background-color: #f8f9fa;'>
                                <td style='padding: 10px; border: 1px solid #dee2e6; font-weight: bold;'>Tur Adı:</td>
                                <td style='padding: 10px; border: 1px solid #dee2e6;'>{WebUtility.HtmlEncode(tourTitle)}</td>
                            </tr>
                            <tr>
                                <td style='padding: 10px; border: 1px solid #dee2e6; font-weight: bold;'>Kişi Sayısı:</td>
                                <td style='padding: 10px; border: 1px solid #dee2e6;'>{personCount} Kişi</td>
                            </tr>
                            <tr style='background-color: #f8f9fa;'>
                                <td style='padding: 10px; border: 1px solid #dee2e6; font-weight: bold;'>Rezervasyon Tarihi:</td>
                                <td style='padding: 10px; border: 1px solid #dee2e6;'>{reservationDate:dd.MM.yyyy HH:mm}</td>
                            </tr>
                            <tr>
                                <td style='padding: 10px; border: 1px solid #dee2e6; font-weight: bold;'>Toplam Tutar:</td>
                                <td style='padding: 10px; border: 1px solid #dee2e6; font-size: 16px; color: #1a7ab5; font-weight: bold;'>{totalPrice:N0} ₺</td>
                            </tr>
                            <tr style='background-color: #f8f9fa;'>
                                <td style='padding: 10px; border: 1px solid #dee2e6; font-weight: bold;'>Durum:</td>
                                <td style='padding: 10px; border: 1px solid #dee2e6; font-weight: bold; color: #28a745;'>{WebUtility.HtmlEncode(reservationStatus)}</td>
                            </tr>
                        </table>

                        <p>Rezervasyonunuz onaylandığında tarafınıza bilgilendirme e-postası gönderilecektir.</p>
                        <p style='margin-top: 30px; font-size: 13px; color: #777;'>İyi yolculuklar dileriz,<br><strong>Vitour Ekibi</strong></p>
                    </div>
                </div>";

            await SendEmailAsync(toEmail, subject, body);
        }

        public async Task SendStatusUpdateEmailAsync(
            string toEmail,
            string nameSurname,
            string tourTitle,
            string newStatus)
        {
            var subject = $"Vitour - Rezervasyon Durumunuz Güncellendi: {newStatus}";
            var body = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 8px; overflow: hidden;'>
                    <div style='background-color: #1a7ab5; color: white; padding: 20px; text-align: center;'>
                        <h1 style='margin: 0; font-size: 24px;'>Vitour Turizm</h1>
                        <p style='margin: 5px 0 0; font-size: 14px;'>Rezervasyon Güncellemesi</p>
                    </div>
                    <div style='padding: 24px; color: #333;'>
                        <p>Sayın <strong>{WebUtility.HtmlEncode(nameSurname)}</strong>,</p>
                        <p><strong>{WebUtility.HtmlEncode(tourTitle)}</strong> turuna ait rezervasyonunuzun durumu güncellenmiştir.</p>
                        
                        <div style='background-color: #f8f9fa; border-left: 4px solid #1a7ab5; padding: 15px; margin: 20px 0;'>
                            <p style='margin: 0; font-size: 16px;'>Yeni Durum: <strong>{WebUtility.HtmlEncode(newStatus)}</strong></p>
                        </div>

                        <p style='margin-top: 30px; font-size: 13px; color: #777;'>İyi yolculuklar dileriz,<br><strong>Vitour Ekibi</strong></p>
                    </div>
                </div>";

            await SendEmailAsync(toEmail, subject, body);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                var smtpHost = _configuration["MailSettings:SmtpHost"] ?? "smtp.gmail.com";
                var portStr = _configuration["MailSettings:SmtpPort"];
                var port = int.TryParse(portStr, out var p) ? p : 587;
                var senderEmail = _configuration["MailSettings:SenderEmail"] ?? "noreply@vitour.com";
                var senderPassword = _configuration["MailSettings:SenderPassword"] ?? "";
                var enableSsl = _configuration.GetValue<bool>("MailSettings:EnableSsl", true);

                if (string.IsNullOrWhiteSpace(toEmail) || !toEmail.Contains("@"))
                    return;

                using var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail, "Vitour Turizm");
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = htmlBody;
                mailMessage.IsBodyHtml = true;

                using var smtpClient = new SmtpClient(smtpHost, port);
                smtpClient.EnableSsl = enableSsl;
                smtpClient.UseDefaultCredentials = false;

                if (!string.IsNullOrWhiteSpace(senderPassword))
                {
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                }

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Email successfully sent to {ToEmail}", toEmail);
            }
            catch (Exception ex)
            {
                // Silently log exception so mail failures do not block the main application flow
                _logger.LogWarning(ex, "Failed to send email to {ToEmail}", toEmail);
            }
        }
    }
}
