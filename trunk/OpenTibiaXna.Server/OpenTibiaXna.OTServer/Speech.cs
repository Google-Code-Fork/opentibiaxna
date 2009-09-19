using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTibiaXna.OTServer
{
    public class Speech
    {
        public SpeechType Type { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public ChatChannel ChannelId { get; set; }
    }
}
