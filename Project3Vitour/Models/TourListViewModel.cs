using Project3Vitour.Dtos.TourDtos;

namespace Project3Vitour.Models
{
    // Tur listeleme sayfasi: sayfalanmis turlar + kartlarda gosterilecek iliskili veriler
    public class TourListViewModel
    {
        public List<ResultTourDto> Tours { get; set; } = new();

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        // Aktif kategori filtresi (yoksa null)
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool HasCategoryFilter => !string.IsNullOrWhiteSpace(CategoryId);

        public int TotalPages => PageSize > 0
            ? (int)Math.Ceiling(TotalCount / (double)PageSize)
            : 0;

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        // TourId -> ilgili veri (kartta gostermek icin)
        public Dictionary<string, string> CategoryNames { get; set; } = new();
        public Dictionary<string, string> DestinationNames { get; set; } = new();
        public Dictionary<string, double> AverageScores { get; set; } = new();
        public Dictionary<string, int> ReviewCounts { get; set; } = new();
    }
}
