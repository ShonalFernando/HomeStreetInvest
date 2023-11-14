using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.Model
{
    public class Feed
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string FeID { get; set; } = null!;
        public string username { get; set; } = null!;
        public string heading { get; set; } = null!;
        public string content { get; set; } = null!;
        public DateTime datePosted { get; set; }
        public string description { get; set; } = null!;
    }
}
