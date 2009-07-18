using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace OpenTibiaXna.Library.Desktop.SpriteEngine
{
    public class SpriteReader
    {
        public static void ReadSprite()
        {
            string datasize = "";

            using (FileStream fs = File.OpenRead(@"C:\b.otxs"))
            {
                byte[] array = new byte[2];

                while (fs.Position < fs.Length)
                {
                    fs.Read(array, 0, 2);
                    datasize += BitConverter.ToUInt16(array, 0).ToString();
                }
            }

            File.WriteAllText(@"C:\c.txt", datasize);
        }

        // Thanks to OpiF at http://otfans.net/showthread.php?t=102065
        public static Image GetSpriteImage(string file, int spriteId)
        {
            if (spriteId < 2 || spriteId > 28722)
                throw new ArgumentOutOfRangeException("spriteId");

            int size = 32;
            Bitmap bitmap = new Bitmap(size, size);
            using (FileStream fs = File.OpenRead(file))
            {
                byte[] array = new byte[4];

                fs.Seek(6 + (spriteId - 1) * 4, SeekOrigin.Begin);

                fs.Read(array, 0, 4);
                uint address = BitConverter.ToUInt32(array, 0);

                fs.Seek(address + 3, SeekOrigin.Begin);

                fs.Read(array, 0, 2);
                ushort datasize = BitConverter.ToUInt16(array, 0);

                int counter = 0;
                int read = 0;
                while (read < datasize)
                {
                    fs.Read(array, 0, 2);
                    ushort transparentPixels = BitConverter.ToUInt16(array, 0);

                    fs.Read(array, 0, 2);
                    ushort coloredPixels = BitConverter.ToUInt16(array, 0);

                    read += 4;
                    counter += transparentPixels;

                    for (int i = 0; i < coloredPixels; i++)
                    {
                        fs.Read(array, 0, 3);
                        bitmap.SetPixel(counter % size,
                            counter / size,
                            Color.FromArgb(array[0], array[1], array[2]));
                        counter++;
                    }

                    read += 3 * coloredPixels;
                }
            }
            return bitmap;
        }

        public static void ConvertAllToBitmap(string file, string outputFolder)
        {
            int size = 32;

            for (int spriteId = 2; spriteId < 49479; spriteId++)
            {
                Bitmap bitmap = new Bitmap(size, size);
                using (FileStream fs = File.OpenRead(file))
                {
                    fs.Seek(4, SeekOrigin.Begin);
                    byte[] array = new byte[4];

                    fs.Seek(6 + (spriteId - 1) * 4, SeekOrigin.Begin);

                    fs.Read(array, 0, 4);
                    uint address = BitConverter.ToUInt32(array, 0);

                    fs.Seek(address + 3, SeekOrigin.Begin);

                    fs.Read(array, 0, 2);
                    ushort datasize = BitConverter.ToUInt16(array, 0);

                    int counter = 0;
                    int read = 0;
                    while (read < datasize)
                    {
                        fs.Read(array, 0, 2);
                        ushort transparentPixels = BitConverter.ToUInt16(array, 0);

                        fs.Read(array, 0, 2);
                        ushort coloredPixels = BitConverter.ToUInt16(array, 0);

                        read += 4;
                        counter += transparentPixels;

                        for (int i = 0; i < coloredPixels; i++)
                        {
                            fs.Read(array, 0, 3);
                            bitmap.SetPixel(counter % size,
                                counter / size,
                                Color.FromArgb(array[0], array[1], array[2]));
                            counter++;
                        }

                        read += 3 * coloredPixels;
                    }
                }

                bitmap.Save(String.Format(@"{0}\{1}.bmp", outputFolder, spriteId));
            }
        }
    }
}
