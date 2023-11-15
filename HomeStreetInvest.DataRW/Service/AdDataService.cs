using DeepFrees.Scheduler.Model;
using HomeStreetInvest.Model;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace HomeStreetInvest.DataRW.Service
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

        public async Task<Advertisment?> GetAsync(string AdID) => //PerAd
            await _Advertisments.Find(x => x.AdID == AdID).FirstOrDefaultAsync();

        public async Task CreateAsync(Advertisment Advertisment) =>
        await _Advertisments.InsertOneAsync(Advertisment);

        public async Task UpdateAsync(string AdID, Advertisment Advertisment) =>
            await _Advertisments.ReplaceOneAsync(x => x.AdID == AdID, Advertisment);

        public async Task RemoveAsync(string AdID) =>
            await _Advertisments.DeleteOneAsync(x => x.AdID == AdID);
    }
}
