using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenTibiaXna.Library.Desktop.SpriteEngine
{
    public class SpriteWriter
    {
        public static void Write()
        {
            byte[] b = new byte[0];
            ushort a = 0;
            b = BitConverter.GetBytes(a);
            File.WriteAllBytes(@"C:\b.sprx", b);
        }
    }
}
