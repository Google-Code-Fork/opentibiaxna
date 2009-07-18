using System;

namespace OpenTibiaXna
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (OpenTibiaXnaGame game = new OpenTibiaXnaGame())
            {
                game.Run();
            }
        }
    }
}

