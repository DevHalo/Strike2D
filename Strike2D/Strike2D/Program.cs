using System;
using System.Collections.Generic;

namespace Strike2D
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Strike2DMain())
                game.Run();
        }
    }
}