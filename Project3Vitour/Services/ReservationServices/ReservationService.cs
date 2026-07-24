using AutoMapper;
using MongoDB.Driver;
using Project3Vitour.Dtos.ReservationDtos;
using Project3Vitour.Entities;
using Project3Vitour.Settings;

namespace Project3Vitour.Services.ReservationServices
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Reservation> _reservationCollection;

        public ReservationService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _reservationCollection = database.GetCollection<Reservation>(_databaseSettings.ReservationCollectionName);
            _mapper = mapper;
        }

        public async Task CreateReservationAsync(CreateReservationDto createReservationDto)
        {
            var value = _mapper.Map<Reservation>(createReservationDto);
            await _reservationCollection.InsertOneAsync(value);
        }

        public async Task DeleteReservationAsync(string id)
        {
            await _reservationCollection.DeleteOneAsync(x => x.ReservationId == id);
        }

        public async Task<List<ResultReservationDto>> GetAllReservationAsync()
        {
            var values = await _reservationCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultReservationDto>>(values);
        }

        public async Task<List<ResultReservationByTourIdDto>> GetAllReservationsByTourIdAsync(string id)
        {
            var values = await _reservationCollection.Find(x => x.TourId == id).ToListAsync();
            return _mapper.Map<List<ResultReservationByTourIdDto>>(values);
        }

        public async Task<GetReservationByIdDto> GetReservationByIdAsync(string id)
        {
            var value = await _reservationCollection.Find(x => x.ReservationId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetReservationByIdDto>(value);
        }

        // Kontenjan kontrolu icin: iptal edilmemis rezervasyonlarin toplam kisi sayisi
        public async Task<int> GetReservedSeatCountByTourIdAsync(string id)
        {
            var values = await _reservationCollection
                .Find(x => x.TourId == id && x.ReservationStatus != ReservationStatuses.Cancelled)
                .ToListAsync();
            return values.Sum(x => x.PersonCount);
        }

        public async Task UpdateReservationAsync(UpdateReservationDto updateReservationDto)
        {
            var value = _mapper.Map<Reservation>(updateReservationDto);
            await _reservationCollection.FindOneAndReplaceAsync(x => x.ReservationId == updateReservationDto.ReservationId, value);
        }
    }
}
