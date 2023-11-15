using DeepFrees.Scheduler.Model;
using HomeStreetInvest.Model;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace Homsin.Service
{
    public class AdDataService
    {
        private readonly IMongoCollection<Advertisment> _Advertisments;

        public AdDataService(IOptions<DatabaseSettings> HSDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                HSDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                HSDatabaseSettings.Value.DatabaseName);

            _Advertisments = mongoDatabase.GetCollection<Advertisment>(
                HSDatabaseSettings.Value.HSDBC[3]);
        }

        public async Task<List<Advertisment>> GetAsync() => //All
            await _Advertisments.Find(_ => true).ToListAsync();

        public async Task<Advertisment?> GetAsync(ObjectId _id) => //PerAd
            await _Advertisments.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(Advertisment Advertisment) =>
        await _Advertisments.InsertOneAsync(Advertisment);

        public async Task UpdateAsync(ObjectId _id, Advertisment Advertisment) =>
            await _Advertisments.ReplaceOneAsync(x => x._id == _id, Advertisment);

        public async Task RemoveAsync(ObjectId _id) =>
            await _Advertisments.DeleteOneAsync(x => x._id == _id);
    }
}
