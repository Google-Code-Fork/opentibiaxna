using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Engines;
using System.Threading;
using OpenTibiaXna.OTServer.Logging;

namespace OpenTibiaXna.OTServer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggingEngine.OnLogError += new LoggingEngine.OnLogErrorHandler(LoggingEngine_OnLogError);
            LoggingEngine.OnLogMessage += new LoggingEngine.OnLogMessageHandler(LoggingEngine_OnLogMessage);
            LoggingEngine.OnLogStart += new LoggingEngine.OnLogStartHandler(LoggingEngine_OnLogStart);
            LoggingEngine.OnLogDone += new LoggingEngine.OnLogDoneHandler(LoggingEngine_OnLogDone);

            ServerEngine.Run();

            Console.ReadLine();
        }

        static void LoggingEngine_OnLogError(LogErrorException exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(exception.UserMessage);
            Console.ResetColor();
        }

        static void LoggingEngine_OnLogDone(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);
            Console.ResetColor();
        }

        static void LoggingEngine_OnLogStart(string message)
        {
            Console.Write(message);
        }

        static void LoggingEngine_OnLogMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
