using Ifuel.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Ifuel.Services
{
    public class FuelQueueService
    {
        //creating the database connection
        private readonly IMongoCollection<FuelQueue> _fuelQueueCollection;
        public FuelQueueService(IOptions<FuelQueueDatabaseSettings> fuelQueueDatabaseSettings)
        {
            MongoClient client = new MongoClient(
                fuelQueueDatabaseSettings.Value.ConnectionString);

            IMongoDatabase database = client.GetDatabase(
                fuelQueueDatabaseSettings.Value.DatabaseName);

            _fuelQueueCollection = database.GetCollection<FuelQueue>(
                fuelQueueDatabaseSettings.Value.FuelQueueName);
        }

        // create a new fuel station
        public async Task CreateAsync(FuelQueue fuelqueue){
            await _fuelQueueCollection.InsertOneAsync(fuelqueue);
            return;
        }

        // get all fuel stations
        public async Task <List<FuelQueue>> GetAsync() {
            return await _fuelQueueCollection.Find(new BsonDocument()).ToListAsync();
        }

        //get fuel queue point using id 
        public async Task<FuelQueue> GetByIdAsync(string id) {
            return await _fuelQueueCollection.Find( c => c.Id == id).SingleAsync();
        }

        // update user departure
        public async Task UpdateFuelQueueAsync(string id, string departureTime, bool fuelPumpStatus ) {
            FilterDefinition<FuelQueue> filter = Builders<FuelQueue>.Filter.Eq("Id", id);
            UpdateDefinition<FuelQueue> update = Builders<FuelQueue>.Update.Set( "departureTime", departureTime).Set("fuelPumpStatus", fuelPumpStatus);
            await _fuelQueueCollection.UpdateOneAsync(filter, update);
            return;
        }
    
        public async Task DeleteAsync(string id){
            FilterDefinition<FuelQueue> filter = Builders<FuelQueue>.Filter.Eq("Id", id); 
            await _fuelQueueCollection.DeleteOneAsync(filter);
            return;
        }

        // // update user fual pump status
        // public async Task UpdateFuelPumpStatusAsync(string id, string fuelPumpStatus ) {
        //     FilterDefinition<FuelQueue> filter = Builders<FuelQueue>.Filter.Eq("Id", id);
        //     UpdateDefinition<FuelQueue> update = Builders<FuelQueue>.Update.Set( "fuelPumpStatus", fuelPumpStatus);
        //     await _collection.UpdateOneAsync(filter, update);
        //     return;
        // }

        // //This is required to create a fuel object
        // public async Task<FuelStation> Create(FuelStation request)
        // {
        //     FuelModel fuelModel = new FuelModel();
        //     fuelModel.Id = ObjectId.GenerateNewId().ToString();
        //     fuelModel.Type = request.Type.ToString();
        //     fuelModel.Amount = request.Amount;
        //     fuelModel.Date = request.Date;
        //     fuelModel.Time = request.Time;
        //     fuelModel.LastModified = DateTime.Now.ToString();
        //     fuelModel.StationsId = request.StationsId ?? null;

        //     var firstStationFilter = Builders<FuelStationModel>.Filter.Eq(a => a.Id, request.StationsId);
        //     var multiUpdateDefinition = Builders<FuelStationModel>.Update
        //         .Push(u => u.Fuel, fuelModel);
        //     var pushNotificationsResult = await _Collection.UpdateOneAsync(firstStationFilter, multiUpdateDefinition);
        //     var results = _Collection.Find(i => i.Id == request.StationsId).ToList();

        //     return results[0];
        // }

    }
}