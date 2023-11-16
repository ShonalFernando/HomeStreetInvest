using DeepFrees.Scheduler.Model;
using HomeStreetInvest.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homsin.Service
{
    public class MessageDataService
    {
        private readonly IMongoCollection<Chat> _Chats;

        public MessageDataService(IOptions<DatabaseSettings> HSDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                HSDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                HSDatabaseSettings.Value.DatabaseName);

            _Chats = mongoDatabase.GetCollection<Chat>(
                HSDatabaseSettings.Value.HSDBC[6]);
        }

        public async Task<List<Chat>> GetAsync() => //All
            await _Chats.Find(_ => true).ToListAsync();

        public async Task<Chat> GetAsync(ObjectId _id) => //Perfd
            await _Chats.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(Chat Chat) =>
        await _Chats.InsertOneAsync(Chat);

        public async Task UpdateAsync(ObjectId _id, Chat Chat) =>
            await _Chats.ReplaceOneAsync(x => x._id == _id, Chat);

        public async Task RemoveAsync(ObjectId _id) =>
            await _Chats.DeleteOneAsync(x => x._id == _id);
    }
}
