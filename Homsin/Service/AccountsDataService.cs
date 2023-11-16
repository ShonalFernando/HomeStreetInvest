using DeepFrees.Scheduler.Model;
using HomeStreetInvest.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Homsin.Service
{
    public class AccountsDataService
    {
        private readonly IMongoCollection<UserAccount> _UserAccounts;

        public AccountsDataService(IOptions<DatabaseSettings> HSDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                HSDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                HSDatabaseSettings.Value.DatabaseName);

            _UserAccounts = mongoDatabase.GetCollection<UserAccount>(
                HSDatabaseSettings.Value.HSDBC[1]);
        }

        public async Task<List<UserAccount>> GetAsync() =>
            await _UserAccounts.Find(_ => true).ToListAsync();

        public async Task<UserAccount?> GetAsync(ObjectId _id) =>
            await _UserAccounts.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(UserAccount userAccount) =>
        await _UserAccounts.InsertOneAsync(userAccount);

        public async Task UpdateAsync(ObjectId _id, UserAccount userAccount) =>
            await _UserAccounts.ReplaceOneAsync(x => x._id == _id, userAccount);

        public async Task RemoveAsync(ObjectId _id) =>
            await _UserAccounts.DeleteOneAsync(x => x._id == _id);
    }
}
