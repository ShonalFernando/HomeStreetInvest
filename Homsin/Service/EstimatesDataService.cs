using DeepFrees.Scheduler.Model;
using HomeStreetInvest.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Homsin.Service
{
    public class EstimatesDataService
    {
        private readonly IMongoCollection<Estimate> _Estimates;

        public EstimatesDataService(IOptions<DatabaseSettings> HSDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                HSDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                HSDatabaseSettings.Value.DatabaseName);

            _Estimates = mongoDatabase.GetCollection<Estimate>(
                HSDatabaseSettings.Value.HSDBC[4]);
        }

        public async Task<List<Estimate>> GetAsync() => //All
            await _Estimates.Find(_ => true).ToListAsync();

        public async Task<Estimate?> GetAsync(ObjectId _id) => //Perest
            await _Estimates.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(Estimate Estimate) =>
        await _Estimates.InsertOneAsync(Estimate);

        public async Task UpdateAsync(ObjectId _id, Estimate Estimate) =>
            await _Estimates.ReplaceOneAsync(x => x._id == _id, Estimate);

        public async Task RemoveAsync(ObjectId _id) =>
            await _Estimates.DeleteOneAsync(x => x._id == _id);
    }
}
