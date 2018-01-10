using System;

namespace Strike2D
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Strike2D - " + Manifest.Version + " - " + Manifest.Environment + " - ");
            using (var game = new Strike2D())
                game.Run();
        }
    }
}