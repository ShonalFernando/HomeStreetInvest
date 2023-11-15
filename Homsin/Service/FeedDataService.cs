using DeepFrees.Scheduler.Model;
using HomeStreetInvest.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Homsin.Service
{
    public class FeedDataService
    {
        private readonly IMongoCollection<Feed> _Feeds;

        public FeedDataService(IOptions<DatabaseSettings> HSDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                HSDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                HSDatabaseSettings.Value.DatabaseName);

            _Feeds = mongoDatabase.GetCollection<Feed>(
                HSDatabaseSettings.Value.HSDBC[5]);
        }

        public async Task<List<Feed>> GetAsync() => //All
            await _Feeds.Find(_ => true).ToListAsync();

        public async Task<Feed?> GetAsync(ObjectId _id) => //Perfd
            await _Feeds.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(Feed Feed) =>
        await _Feeds.InsertOneAsync(Feed);

        public async Task UpdateAsync(ObjectId _id, Feed Feed) =>
            await _Feeds.ReplaceOneAsync(x => x._id == _id, Feed);

        public async Task RemoveAsync(ObjectId _id) =>
            await _Feeds.DeleteOneAsync(x => x._id == _id);
    }
}
