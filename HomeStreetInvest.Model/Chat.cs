using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.Model
{
    public class Chat
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public int chatindex { get; set; }
        public string sender { get; set; } = null!; //Username of first sender
        public string reciever { get; set; } = null!; //target username
        public string chatMessage { get; set; } = null!;
    }

}
