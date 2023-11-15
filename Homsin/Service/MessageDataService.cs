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
        private readonly IMongoCollection<ChatHead> _ChatHeads;

        public MessageDataService(IOptions<DatabaseSettings> HSDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                HSDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                HSDatabaseSettings.Value.DatabaseName);

            _ChatHeads = mongoDatabase.GetCollection<ChatHead>(
                HSDatabaseSettings.Value.HSDBC[6]);
        }

        public async Task<List<ChatHead>> GetAsync() => //All
            await _ChatHeads.Find(_ => true).ToListAsync();

        public async Task<ChatHead> GetAsync(ObjectId _id) => //Perfd
            await _ChatHeads.Find(x => x._id == _id).FirstOrDefaultAsync();

        public async Task CreateAsync(ChatHead ChatHead) =>
        await _ChatHeads.InsertOneAsync(ChatHead);

        public async Task UpdateAsync(ObjectId _id, ChatHead ChatHead) =>
            await _ChatHeads.ReplaceOneAsync(x => x._id == _id, ChatHead);

        public async Task RemoveAsync(ObjectId _id) =>
            await _ChatHeads.DeleteOneAsync(x => x._id == _id);
    }
}
