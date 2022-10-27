using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Ifuel.Models
{
    public class FuelQueue
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id {get; set;}
        [BsonElement("userId")]
        public int? UserId {get; set;}
        [BsonElement("userVehicalType")]
        public string? UserVehicalType {get; set;}
        [BsonElement("stationId")]
        public string? StationId {get; set;}
        // [BsonElement("arrivalDate")]
        // public string? ArrivalDate {get; set;}
        [BsonElement("arrivalTime")]
        public string? ArrivalTime {get; set;}
        // [BsonElement("departureDate")]
        // public string? DepartureDate {get; set;}
        [BsonElement("departureTime")]
        public string? DepartureTime {get; set;}
        [BsonElement("fuelPumpStatus")]
        public bool? FuelPumpStatus {get; set;}
    }
}
