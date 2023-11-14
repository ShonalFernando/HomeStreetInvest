using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.Model
{
    public class Advertisment
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string AdID { get; set; } = null!;
        public string? username { get; set; }
        public string? heading { get; set; }
        public string? content { get; set; }
        public DateTime datePosted { get; set; }
        public string? description { get; set; }
    }
}
