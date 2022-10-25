using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class FuelStatus 
    {
      [BsonElement("fuelType")]  
      public string FuelType {get; set;}  = null!;
      [BsonElement("availabilityStatus")]  
      public bool AvailabilityStatus {get; set;}  
      [BsonElement("arrivalDate")]  
      public string ArrivalDate {get; set;} = null!;
      [BsonElement("departureDate")]  
      public string DepartureDate {get; set;} = null!;
    }
