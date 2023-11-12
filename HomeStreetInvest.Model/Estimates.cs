using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.Model
{
    public class Estimates
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public ObjectId ownerid { get; set; }
        public string? heading { get; set; }
        public string? content { get; set; }
        public string? description { get; set; }
        public string? sellprice { get; set; }
        public string? propdata { get; set; }
    }
}
