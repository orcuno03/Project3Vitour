using Project3Vitour.Dtos.TourDtos;

namespace Project3Vitour.Services.TourServices
{
    public interface ITourService
    {
        Task<List<ResultTourDto>> GetAllTourAsync();
        Task CreateTourAsync(CreateTourDto createTourDto);
        Task UpdateTourAsync(UpdateTourDto updateTourDto);
        Task DeleteTourAsync(string id);
        Task<GetTourByIdDto> GetTourByIdAsync(string id);

        // Sitedeki tur listeleme sayfasi icin: sadece yayindaki turlar, sayfalanmis.
        // categoryId bos ise kategori filtresi uygulanmaz.
        Task<List<ResultTourDto>> GetActiveToursWithPagingAsync(int page, int pageSize, string categoryId = null);
        Task<int> GetActiveTourCountAsync(string categoryId = null);
    }
}
