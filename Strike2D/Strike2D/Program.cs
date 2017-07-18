using System;
using System.Collections.Generic;

namespace Strike2D
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Strike2D - " + Manifest.Version + " - " + Manifest.Environment + " -");
            using (var game = new Strike2DMain())
                game.Run();
        }
    }
}