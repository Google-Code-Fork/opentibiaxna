using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer.Logging
{
    [global::System.Serializable]
    public class LogErrorException : Exception
    {
        public LogErrorException() { }
        public LogErrorException(string message) : base(message) { }
        public LogErrorException(string message, Exception inner) : base(message, inner) { }
        protected LogErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Message to be displayed to user
        /// </summary>
        public String UserMessage { get; set; }
    }
}
