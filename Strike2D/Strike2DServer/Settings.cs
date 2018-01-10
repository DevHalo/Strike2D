using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Strike2DServer
{
    public static class Settings
    {
        public static int Port => settings.Port;
        public static int MaxPlayers => settings.MaxPlayers;
        public static string Map => settings.Map;
        public static int SendRate => settings.SendRate;    // How many times the server sends 
        public static int TickRate => settings.TickRate;

        private static SerializableSettings settings = new SerializableSettings();
        
        public static void LoadSettings()
        {
            if (File.Exists("server.cfg"))
            {
                FieldInfo[] fields = settings.GetType().GetFields();
                StreamReader reader = File.OpenText("server.cfg");

                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine()?.Split(' ');

                    // Should be "key = value"
                    if (line.Length != 3) continue;
                    if (line[1] != "=") continue;

                    // If the setting in the file doesn't exist as a real setting
                    if (fields.All(f => f.Name != line[0]))
                    {
                        continue;
                    }
                    
                    FieldInfo field = fields.First(f => f.Name == line[0]);
                    
                    var value = Cast(line[2], field.FieldType);

                    Console.WriteLine("Writing to " + field.Name + " with value " +
                                           value.ToString());

                    field.SetValue(settings, value);
                }
                
                reader.Close();
                WriteSettings();
            }
            else
            {
                WriteSettings();
            }
        }

        private static void WriteSettings()
        {
            FieldInfo[] fields = settings.GetType().GetFields();

            StreamWriter writer;
            try
            {
                File.Delete("server.cfg");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("No pre-existing settings file.");
            }
            finally
            {
                writer = File.CreateText("server.cfg");

                foreach (FieldInfo field in fields)
                {
                    writer.WriteLine(field.Name + " = " + field.GetValue(settings));
                }

                writer.Close();
            }
        }
    
        /// <summary>
        /// Allows dynamic casting
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static dynamic Cast(dynamic obj, Type type)
        {
            object value = null;
            try { value = Convert.ChangeType(obj, type); }
            catch (InvalidCastException e)
            {
                Console.WriteLine("Unable to cast value to target type");
                value = null;
            }
            return value;
        }
    
    }

    internal class SerializableSettings
    {
        public int Port = 27015;
        public int MaxPlayers = 32;
        public string Map = "";
        public int SendRate = 30;
        public int TickRate = 60;
    }
}