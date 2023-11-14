using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.Model
{
    public class Estimate
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string EsID { get; set; } = null!;
        public string username { get; set; } = null!;
        public string heading { get; set; } = null!;
        public string content { get; set; } = null!;
        public string description { get; set; } = null!;
        public string sellprice { get; set; } = null!;
    }
}
