using DeepFrees.Scheduler.Model;
using HomeStreetInvest.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HomeStreetInvest.UserAccounts.Services
{
    public class LogDataStream
    {
        private readonly IMongoCollection<LogModel> _Logs;

        public LogDataStream(IOptions<DatabaseSettings> HSDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                HSDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                HSDatabaseSettings.Value.DatabaseName);

            _Logs = mongoDatabase.GetCollection<LogModel>(
                HSDatabaseSettings.Value.HSDBC[0]);
        }

        public async Task<List<LogModel>> GetAsync() =>
            await _Logs.Find(_ => true).ToListAsync();

        public async Task<LogModel?> GetAsync(ObjectId _id) =>
            await _Logs.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(LogModel WeeklyTaskSolution) =>
        await _Logs.InsertOneAsync(WeeklyTaskSolution);

        public async Task UpdateAsync(ObjectId _id, LogModel LogModel) =>
            await _Logs.ReplaceOneAsync(x => x._id == _id, LogModel);

        public async Task RemoveAsync(ObjectId _id) =>
            await _Logs.DeleteOneAsync(x => x._id == _id);
    }
}
