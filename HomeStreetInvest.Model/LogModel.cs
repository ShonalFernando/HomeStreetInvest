using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.Model
{
    public class LogModel
    {
        [BsonId]
        public ObjectId? _id { get; set; }
        public DateTime timePrint { get; set; }
        public string? logHead { get; set; }
        public string? logMessage { get; set; }
        public string? args { get; set; }
        public LogWarning logWarning { get; set; }
        public Exception? exception { get; set; }
    }

    public enum LogWarning
    {
        DefaultLog,
        Error,
        Warning,
        Info
    }
}
