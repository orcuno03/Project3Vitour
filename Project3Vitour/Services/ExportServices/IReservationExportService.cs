using Project3Vitour.Dtos.ReservationDtos;

namespace Project3Vitour.Services.ExportServices
{
    public interface IReservationExportService
    {
        byte[] GenerateExcel(string tourTitle, List<ResultReservationByTourIdDto> reservations);
        byte[] GeneratePdf(string tourTitle, List<ResultReservationByTourIdDto> reservations);
    }
}
