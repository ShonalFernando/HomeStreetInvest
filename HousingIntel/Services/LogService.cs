using Amazon.Runtime.Internal.Util;
using HomeStreetInvest.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.HousingIntel.Services
{
    public class LogService
    {
        LogDataStream _LogDataStream;

        public LogService(LogDataStream logDataStream)
        {
            _LogDataStream = logDataStream;
        }

        public async void Log(string LogMessage,LogWarning LogWarning)
        {
            LogModel LogModel = new();
            LogModel._id = ObjectId.GenerateNewId();
            LogModel.timePrint = DateTime.Now;
            LogModel.logMessage = LogMessage;
            LogModel.logWarning = LogWarning;

            await _LogDataStream.CreateAsync(LogModel);
        }

        public async void Log(Exception exception)
        {
            LogModel LogModel = new();
            LogModel._id = ObjectId.GenerateNewId();
            LogModel.timePrint = DateTime.Now;
            LogModel.exception = exception;

            await _LogDataStream.CreateAsync(LogModel);
        }
    }
}
