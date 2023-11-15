using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.Model
{
    public class Chat
    {
        public int index { get; set; }
        public bool issender1 { get; set; } //TO d
        public string chatMessage { get; set;} = null!;
        public bool isSeen { get; set; }
    }

    public class ChatHead
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string sender1 { get; set; } = null!; //Username of first sender
        public string sender2 { get; set; } = null!; //target username
        public List<Chat>? chats { get; set; }
    }

}
