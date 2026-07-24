using AutoMapper;
using MongoDB.Driver;
using Project3Vitour.Dtos.TourImageDtos;
using Project3Vitour.Entities;
using Project3Vitour.Settings;

namespace Project3Vitour.Services.TourImageServices
{
    public class TourImageService : ITourImageService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<TourImage> _tourImageCollection;

        public TourImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _tourImageCollection = database.GetCollection<TourImage>(_databaseSettings.TourImageCollectionName);
            _mapper = mapper;
        }

        public async Task CreateTourImageAsync(CreateTourImageDto createTourImageDto)
        {
            var value = _mapper.Map<TourImage>(createTourImageDto);
            await _tourImageCollection.InsertOneAsync(value);
        }

        public async Task DeleteTourImageAsync(string id)
        {
            await _tourImageCollection.DeleteOneAsync(x => x.TourImageId == id);
        }

        public async Task<List<ResultTourImageDto>> GetAllTourImageAsync()
        {
            var values = await _tourImageCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultTourImageDto>>(values);
        }

        public async Task<List<ResultTourImageByTourIdDto>> GetAllTourImagesByTourIdAsync(string id)
        {
            var values = await _tourImageCollection.Find(x => x.TourId == id).ToListAsync();
            return _mapper.Map<List<ResultTourImageByTourIdDto>>(values);
        }

        public async Task<GetTourImageByIdDto> GetTourImageByIdAsync(string id)
        {
            var value = await _tourImageCollection.Find(x => x.TourImageId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetTourImageByIdDto>(value);
        }

        public async Task UpdateTourImageAsync(UpdateTourImageDto updateTourImageDto)
        {
            var value = _mapper.Map<TourImage>(updateTourImageDto);
            await _tourImageCollection.FindOneAndReplaceAsync(x => x.TourImageId == updateTourImageDto.TourImageId, value);
        }
    }
}
