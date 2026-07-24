using Project3Vitour.Dtos.TourPlanDtos;

namespace Project3Vitour.Services.TourPlanServices
{
    public interface ITourPlanService
    {
        Task<List<ResultTourPlanDto>> GetAllTourPlanAsync();
        Task CreateTourPlanAsync(CreateTourPlanDto createTourPlanDto);
        Task UpdateTourPlanAsync(UpdateTourPlanDto updateTourPlanDto);
        Task DeleteTourPlanAsync(string id);
        Task<GetTourPlanByIdDto> GetTourPlanByIdAsync(string id);
        Task<List<ResultTourPlanByTourIdDto>> GetAllTourPlansByTourIdAsync(string id);
    }
}
