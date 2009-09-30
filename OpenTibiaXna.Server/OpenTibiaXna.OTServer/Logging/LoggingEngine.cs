using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Helpers;

namespace OpenTibiaXna.OTServer.Logging
{
    public class LoggingEngine
    {
        #region Fields

        private static int logStartTick, startMessageLength;

        #endregion

        #region Methods

        private static string GetLogHeader(string message)
        {
            logStartTick = System.Environment.TickCount;
            return String.Format("\n{0} {1}",
                                DateTime.Now,
                                message);
        }

        public static void LogStart(string message)
        {
            message = GetLogHeader(message);
            startMessageLength = message.Length + DateTime.Now.ToString().Length;

            if (OnLogStart != null)
                OnLogStart(message);
        }

        public static void LogDone()
        {
            int timeElapsed = System.Environment.TickCount - logStartTick;
            string doneTime = (timeElapsed < 1000) ?
                                String.Format("({0} ms)", timeElapsed) :
                                String.Format("({0:0.00} s)", timeElapsed / 1000.0);

            string message = String.Format("{0} {1} {2}",
                                            ".".Repeat(75 - startMessageLength),
                                            "Done",
                                            doneTime).ToString();
            if (OnLogDone != null)
                OnLogDone(message);
        }

        public static void LogMessage(string message)
        {
            message = GetLogHeader(String.Format("-> Message: {0}", message));

            if (OnLogMessage != null)
                OnLogMessage(message);
        }

        public static void LogError(LogErrorException exeption)
        {
            exeption.UserMessage = GetLogHeader(String.Format("-> Error: {0}", exeption.Message));

            if (OnLogError != null)
                OnLogError(exeption);
        }

        #endregion

        #region Events Declaration

        public delegate void OnLogStartHandler(string message);
        public static event OnLogStartHandler OnLogStart;

        public delegate void OnLogDoneHandler(string message);
        public static event OnLogDoneHandler OnLogDone;

        public delegate void OnLogErrorHandler(LogErrorException exception);
        public static event OnLogErrorHandler OnLogError;

        public delegate void OnLogMessageHandler(string message);
        public static event OnLogMessageHandler OnLogMessage;

        #endregion
    }
}
