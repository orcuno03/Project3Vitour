using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Project3Vitour.Dtos.TourDtos;
using Project3Vitour.Entities;
using Project3Vitour.Settings;

namespace Project3Vitour.Services.TourServices
{
    public class TourService : ITourService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Tour> _tourCollection;

        public TourService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
            _mapper = mapper;
        }

        public async Task CreateTourAsync(CreateTourDto createTourDto)
        {
            var value = _mapper.Map<Tour>(createTourDto);
            await _tourCollection.InsertOneAsync(value);
        }

        public async Task DeleteTourAsync(string id)
        {
            await _tourCollection.DeleteOneAsync(x => x.TourId == id);
        }

        public async Task<List<ResultTourDto>> GetAllTourAsync()
        {
            var values = await _tourCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(values);
        }

        public async Task<GetTourByIdDto> GetTourByIdAsync(string id)
        {
            // TourId ObjectId olarak saklaniyor; gecersiz bir id ile filtre kurmak
            // surucude FormatException'a yol aciyor. Bulunamadi gibi ele aliniyor.
            if (!ObjectId.TryParse(id, out _))
                return null;

            var value = await _tourCollection.Find(x => x.TourId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetTourByIdDto>(value);
        }

        public async Task<List<ResultTourDto>> GetActiveToursWithPagingAsync(int page, int pageSize, string categoryId = null)
        {
            // Sirasiz Skip/Limit'te belge guncellendiginde sira kayabilir ve ayni tur
            // iki sayfada birden cikabilir; bu yuzden sayfalamadan once sabit siralama.
            var values = await _tourCollection.Find(ActiveTourFilter(categoryId))
                                              .SortByDescending(x => x.TourId)
                                              .Skip((page - 1) * pageSize)
                                              .Limit(pageSize)
                                              .ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(values);
        }

        public async Task<int> GetActiveTourCountAsync(string categoryId = null)
        {
            var count = await _tourCollection.CountDocumentsAsync(ActiveTourFilter(categoryId));
            return (int)count;
        }

        public async Task UpdateTourAsync(UpdateTourDto updateTourDto)
        {
            var values = _mapper.Map<Tour>(updateTourDto);
            await _tourCollection.FindOneAndReplaceAsync(x => x.TourId == updateTourDto.TourId, values);
        }

        private static FilterDefinition<Tour> ActiveTourFilter(string categoryId)
        {
            var builder = Builders<Tour>.Filter;
            var filter = builder.Eq(x => x.IsStatus, true);

            if (!string.IsNullOrWhiteSpace(categoryId))
                filter &= builder.Eq(x => x.CategoryId, categoryId);

            return filter;
        }
    }
}
