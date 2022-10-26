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

        public async Task<FuelStation> GetByIdAsync(string id) {
            return await _fuelStationCollection.Find( c => c.Id == id).SingleAsync();
        }

        // update fual status in selected fuel station
        // public async Task UpdateFuelStatusAsync(string id, FuelStatus[] fuelStatuses) {
        //     FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("Id", id);
        //     UpdateDefinition<FuelStation> update = Builders<FuelStation>.Update.Set("fuelStatuses",fuelStatuses);
        //     await _fuelStationCollection.UpdateOneAsync(filter, update);
        //     return;
        // }

        public async Task UpdateFuelStatusAsync(string id, FuelStatus[] fuelStatuses) {
            FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("Id", id);
            UpdateDefinition<FuelStation> update = Builders<FuelStation>.Update.Set("fuelStatuses",fuelStatuses);
            await _fuelStationCollection.UpdateOneAsync(filter, update);
            return;
        }
    

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