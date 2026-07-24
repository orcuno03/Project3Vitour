using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Vitour.Entities
{
    public class TourImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TourImageId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string TourId { get; set; }
    }
}
