using Ifuel.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Ifuel.Services
{
    public class FuelStationService
    {
        //creating the database connection
        private readonly IMongoCollection<FuelStation> _fuelStationCollection;
        public FuelStationService(IOptions<FuelStationDatabaseSettings> fuelStationDatabaseSettings)
        {
            MongoClient client = new MongoClient(
                fuelStationDatabaseSettings.Value.ConnectionString);

            IMongoDatabase database = client.GetDatabase(
                fuelStationDatabaseSettings.Value.DatabaseName);

            _fuelStationCollection = database.GetCollection<FuelStation>(
                fuelStationDatabaseSettings.Value.FuelStationName);
        }

        // create a new fuel station
        public async Task CreateAsync(FuelStation fuelstation){
            await _fuelStationCollection.InsertOneAsync(fuelstation);
            return;
        }

        // get all fuel stations
        public async Task <List<FuelStation>> GetAsync() {
            return await _fuelStationCollection.Find(new BsonDocument()).ToListAsync();
        }

        // get fuel station details by id 
        public async Task<FuelStation> GetByIdAsync(string id) {
            return await _fuelStationCollection.Find( c => c.Id == id).SingleAsync();
        }

        // update fuel status
        public async Task UpdateFuelStatusAsync(string id, FuelStatus[] fuelStatuses) {
            FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("Id", id);
            UpdateDefinition<FuelStation> update = Builders<FuelStation>.Update.Set("fuelStatuses",fuelStatuses);
            await _fuelStationCollection.UpdateOneAsync(filter, update);
            return;
        }

    }
}