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
            #region LogginEngine Events

            // Sample: Simple logging without colors
            // LoggingEngine.OnLog += message => Console.Write(message);

            // Sample: Colorfull logging
            LoggingEngine.OnLogStart += message => Console.Write(message);

            LoggingEngine.OnLogError += exception =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(exception.UserMessage);
                Console.ResetColor();
            };

            LoggingEngine.OnLogMessage += message =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(message);
                Console.ResetColor();
            };

            LoggingEngine.OnLogDone += message =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(message);
                Console.ResetColor();
            };

            #endregion

            ServerEngine.Run();
            Console.WriteLine();

            bool exit = false;
            while (!exit)
            {
                if (Console.ReadLine().ToLower().Equals("exit"))
                    exit = true;
            }
        }
    }
}
