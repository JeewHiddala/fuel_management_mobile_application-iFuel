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

        // get the queue length
        public async Task<FuelQueue> GetQueueLengthAsync(string userVehicalType){
            var res = await _fuelQueueCollection.Find(c => c.UserVehicalType == userVehicalType).ToListAsync();
            var queueLength = res.length();
            return queueLength;
        }

        // get the queue time
            public async Task<FuelQueue> GetQueueTimeAsync(QueueModel queue){
            var arivalTime = DateTime.Parse(queue.ArivalTime).ToString("hh:mm tt");
            var currentTime = DateTime.Now.ToString("hh:mm tt");
            var timeDifferance = DateTime.Parse(currentTime).Subtract(DateTime.Parse(arivalTime));
            return timeDifferance.ToString();
        }

        // update user departure
        public async Task UpdateFuelQueueAsync(string id, string departureTime, bool fuelPumpStatus ) {
            FilterDefinition<FuelQueue> filter = Builders<FuelQueue>.Filter.Eq("Id", id);
            UpdateDefinition<FuelQueue> update = Builders<FuelQueue>.Update.Set( "departureTime", departureTime).Set("fuelPumpStatus", fuelPumpStatus);
            await _fuelQueueCollection.UpdateOneAsync(filter, update);
            return;
        }
    
        // delete past queue item
        public async Task DeleteAsync(string id){
            FilterDefinition<FuelQueue> filter = Builders<FuelQueue>.Filter.Eq("Id", id); 
            await _fuelQueueCollection.DeleteOneAsync(filter);
            return;
        }
    }
}