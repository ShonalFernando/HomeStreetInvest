using DeepFrees.Scheduler.Model;
using HomeStreetInvest.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HomeStreetInvest.DataRW.Service
{
    public class FeedDataService
    {
        private readonly IMongoCollection<Feed> _Estimates;

        public FeedDataService(IOptions<DatabaseSettings> HSDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                HSDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                HSDatabaseSettings.Value.DatabaseName);

            _Estimates = mongoDatabase.GetCollection<Feed>(
                HSDatabaseSettings.Value.HSDBC[5]);
        }

        public async Task<List<Feed>> GetAsync() => //All
            await _Estimates.Find(_ => true).ToListAsync();

        public async Task<Feed?> GetAsync(ObjectId _id) => //Perfd
            await _Estimates.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(Feed Feed) =>
        await _Estimates.InsertOneAsync(Feed);

        public async Task UpdateAsync(ObjectId _id, Feed Feed) =>
            await _Estimates.ReplaceOneAsync(x => x._id == _id, Feed);

        public async Task RemoveAsync(ObjectId _id) =>
            await _Estimates.DeleteOneAsync(x => x._id == _id);
    }
}
