// Console debugging which wraps the standard command line with the in-game console

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D.DevTools
{
    /// <summary>
    /// Static class which handles console input and output.
    /// </summary>
    public static class Debug
    {
        public enum DebugType
        {
            Logging,
            Network,
            CriticalError,
            Warning
        }
        
        public static List<DebugLine> Log { get; private set; } = new List<DebugLine>();
         
        /// <summary>
        /// Writes to the internal log which can be viewed from the ingame console
        /// </summary>
        /// <param name="message"></param>
        /// <param name="debugType"></param>
        public static void WriteLine(string message, DebugType debugType = DebugType.Logging)
        {
            Log.Add(new DebugLine(message, debugType));
        }

        /// <summary>
        /// Writes to the internal log as well as the command line
        /// </summary>
        public static void WriteLineVerbose(string message, DebugType debugType = DebugType.Logging)
        {
            WriteLine(message, debugType);

            // Do not write to the console in production
            if (Manifest.Environment == Manifest.Release) return;
            
            Console.ForegroundColor = ConsoleColor.White;
            switch (debugType)
            {
                case DebugType.Network:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case DebugType.CriticalError:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case DebugType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
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
            WriteLineVerbose("Dumping log to file...");
            StreamWriter writer = File.CreateText("consolelog.txt");

            foreach (DebugLine line in Log)
            {
                writer.Write("[" + line.Type + "] " + line.Message);
            }
            
            writer.Close();
            WriteLineVerbose("Dump written to consolelog.txt");
        }

        /// <summary>
        /// Prints 
        /// </summary>
        /// <param name="sb"></param>
        public static void DrawDebugInformation(SpriteBatch sb)
        {
            
        }

        /// <summary>
        /// Displays an error box in the event of an exception
        /// </summary>
        /// <param name="exception"></param>
        public static void ThrowException(Exception exception)
        {
            MessageBox.Show(exception.Message + "\n\n" + Environment.StackTrace, "Strike2D.exe ripped",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Container for a line. Contains the logging type and its message
    /// </summary>
    public class DebugLine
    {
        public readonly string Message;
        public readonly Debug.DebugType Type;

        public DebugLine(string message, Debug.DebugType debugType = Debug.DebugType.Logging)
        {
            Message = message;
            Type = debugType;
        }
    }
}