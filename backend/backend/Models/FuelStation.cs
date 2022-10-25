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
        [BsonElement("fuelstatuses")]
        [JsonPropertyName("fuelstatuses")]
        public List<object> fuelStatus {get; set;}  = null!;
        [BsonElement("type")]
        public string location {get; set;}  = null!;

    }
}
