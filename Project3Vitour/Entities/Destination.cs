using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Vitour.Entities
{
    public class Destination
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DestinationId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public bool DestinationStatus { get; set; }
    }
}
