using HomeStreetInvest.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.HSISSXE.Helper
{
    public static class LogService
    {
        public static void Log(string LogMessage, LogWarning LogWarning)
        {
            switch (LogWarning)
            {
                case LogWarning.Info:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case LogWarning.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogWarning.Warning:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case LogWarning.DefaultLog:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
            Console.WriteLine($"HOMESTREETINVEST:\t {LogMessage}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Log(Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"HOMESTREETINVEST:\t {exception.Message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
