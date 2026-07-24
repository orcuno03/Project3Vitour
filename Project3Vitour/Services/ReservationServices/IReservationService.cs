using Project3Vitour.Dtos.ReservationDtos;

namespace Project3Vitour.Services.ReservationServices
{
    public interface IReservationService
    {
        Task<List<ResultReservationDto>> GetAllReservationAsync();
        Task CreateReservationAsync(CreateReservationDto createReservationDto);
        Task UpdateReservationAsync(UpdateReservationDto updateReservationDto);
        Task DeleteReservationAsync(string id);
        Task<GetReservationByIdDto> GetReservationByIdAsync(string id);
        Task<List<ResultReservationByTourIdDto>> GetAllReservationsByTourIdAsync(string id);
        Task<int> GetReservedSeatCountByTourIdAsync(string id);
    }
}
