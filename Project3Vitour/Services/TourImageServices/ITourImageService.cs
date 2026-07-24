using Project3Vitour.Dtos.TourImageDtos;

namespace Project3Vitour.Services.TourImageServices
{
    public interface ITourImageService
    {
        Task<List<ResultTourImageDto>> GetAllTourImageAsync();
        Task CreateTourImageAsync(CreateTourImageDto createTourImageDto);
        Task UpdateTourImageAsync(UpdateTourImageDto updateTourImageDto);
        Task DeleteTourImageAsync(string id);
        Task<GetTourImageByIdDto> GetTourImageByIdAsync(string id);
        Task<List<ResultTourImageByTourIdDto>> GetAllTourImagesByTourIdAsync(string id);
    }
}
