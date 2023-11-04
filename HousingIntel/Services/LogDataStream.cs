using DeepFrees.Scheduler.Model;
using HomeStreetInvest.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace HomeStreetInvest.HousingIntel.Services
{
    public class LogDataStream
    {
        private readonly IMongoCollection<LogModel> _WeeklyTaskSolutions;

        public LogDataStream(IOptions<DatabaseSettings> HSDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                HSDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                HSDatabaseSettings.Value.DatabaseName);

            _WeeklyTaskSolutions = mongoDatabase.GetCollection<LogModel>(
                HSDatabaseSettings.Value.ShoppinzUsersCollectionName[0]);
        }

        public async Task<List<LogModel>> GetAsync() =>
            await _WeeklyTaskSolutions.Find(_ => true).ToListAsync();

        public async Task<LogModel?> GetAsync(ObjectId _id) =>
            await _WeeklyTaskSolutions.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(LogModel WeeklyTaskSolution) =>
        await _WeeklyTaskSolutions.InsertOneAsync(WeeklyTaskSolution);

        public async Task UpdateAsync(ObjectId _id, LogModel LogModel) =>
            await _WeeklyTaskSolutions.ReplaceOneAsync(x => x._id == _id, LogModel);

        public async Task RemoveAsync(ObjectId _id) =>
            await _WeeklyTaskSolutions.DeleteOneAsync(x => x._id == _id);
    }
}
