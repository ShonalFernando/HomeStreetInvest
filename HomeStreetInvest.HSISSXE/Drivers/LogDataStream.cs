using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections;
using HomeStreetInvest.Model;
using HomeStreetInvest.HSISSXE.Helper;

namespace HomeStreetInvest.HSISSXE.Drivers
{
    internal static class LogDataStream
    {
        static readonly string connectionString = "mongodb://localhost:27017";
        static readonly MongoClient client = new MongoClient(connectionString);
        static readonly IMongoDatabase database = client.GetDatabase("HomeStreetInvest");
        static readonly string CollectionName = "Log";

        public static List<LogModel> ReadLog()
        {

            // Access the collection
            IMongoCollection<LogModel> collection = database.GetCollection<LogModel>(CollectionName);

            var ReturnableCollection = collection.Find(l => l.timePrint > AppState.LastLogReadTime).ToList();
            AppState.LastLogReadTime = DateTime.Now;
            return ReturnableCollection;
        }
    }
}
