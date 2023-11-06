using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.Model
{
    public class UserAccount
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string userName { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;

        public string? sessionID { get; set; }

        public string? encKey { get; set; }

        public AccountStatus accountStatus { get; set; }

        public Dictionary<string, string>? userSettings { get;set;}
    }

    public enum AccountStatus
    {
        Valid,
        Banned,
        Invalid,
        Delete
    }
}
