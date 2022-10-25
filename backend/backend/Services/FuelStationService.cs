using Ifuel.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Ifuel.Services
{
    public class FuelStationService
    {
        //creating the database connection
        private readonly IMongoCollection<FuelStation> _collection;
        public FuelStationService(IOptions<Connection> Connection)
        {
            MongoClient client = new MongoClient(
                Connection.Value.ConnectionURI);

            IMongoDatabase database = client.GetDatabase(
                Connection.Value.DatabaseName);

            _collection = database.GetCollection<FuelStation>(
                Connection.Value.CollectionName);
        }

        // create a new fuel station
        public async Task CreateAsync(FuelStation fuelstation){
            await _collection.InsertOneAsync(fuelstation);
            return;
        }

        // get all fuel stations
        public async Task <List<FuelStation>> GetAsync() {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<FuelStation> GetByIdAsync(string id) {
            return await _collection.Find( c => c.Id == id).SingleAsync();
        }

        // update fual status in selected fuel station
        public async Task UpdateFuelStatusAsync(string id, FuelStatus[] fuelStatuses) {
            FilterDefinition<FuelStation> filter = Builders<FuelStation>.Filter.Eq("Id", id);
            UpdateDefinition<FuelStation> update = Builders<FuelStation>.Update.Set("fuelStatuses",fuelStatuses);
            await _collection.UpdateOneAsync(filter, update);
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