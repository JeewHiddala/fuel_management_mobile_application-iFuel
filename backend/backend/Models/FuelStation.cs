using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Ifuel.Models
{
    public class FuelStation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id {get; set;}
        [BsonElement("ownerId")]
        public int? OwnerId {get; set;}
        [BsonElement("fuelStatuses")]
        // [JsonPropertyName("fuelStatuses")]
        public FuelStatus[] FuelStatuses {get; set;}  = null!;
        [BsonElement("location")]
        public string Location {get; set;}  = null!;

    }
}
