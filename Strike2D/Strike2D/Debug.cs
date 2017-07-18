// Console debugging which wraps the standard command line with the in-game console

using System;
using System.Collections.Generic;
using System.IO;

namespace Strike2D
{
    /// <summary>
    /// Static class which handles console input and output.
    /// </summary>
    public static class Debug
    {
        public static List<DebugLine> Log { get; private set; } = new List<DebugLine>();
         
        /// <summary>
        /// Writes to the internal log which can be viewed from the ingame console
        /// </summary>
        /// <param name="message"></param>
        /// <param name="debugType"></param>
        public static void WriteLine(string message, DebugLine.DebugType debugType = DebugLine.DebugType.Logging)
        {
            Log.Add(new DebugLine(message, debugType));
        }

        /// <summary>
        /// Writes to the internal log as well as the command line
        /// </summary>
        public static void WriteLineVerbose(string message, DebugLine.DebugType debugType = DebugLine.DebugType.Logging)
        {
            WriteLine(message, debugType);

            Console.ForegroundColor = ConsoleColor.White;
            switch (debugType)
            {
                case DebugLine.DebugType.Network:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case DebugLine.DebugType.CriticalError:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            
            Console.WriteLine("[" + debugType + "] " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Dumps the internal log to a file
        /// </summary>
        public static void DumpLog()
        {
            Console.WriteLine("Dumping log to file...");
            StreamWriter writer = File.CreateText("consolelog.txt");

            foreach (DebugLine line in Log)
            {
                writer.Write("[" + line.Type + "] " + line.Message);
            }
            
            writer.Close();
            Console.WriteLine("Dump written to consolelog.txt");
        }
    }

    /// <summary>
    /// Container for a line. Contains the logging type and its message
    /// </summary>
    public class DebugLine
    {
        public enum DebugType
        {
            Logging,
            Network,
            CriticalError
        }

        public readonly string Message;
        public readonly DebugType Type;

        public DebugLine(string messsage, DebugType debugType = DebugType.Logging)
        {
            Message = Message;
            Type = debugType;
        }
    }
}