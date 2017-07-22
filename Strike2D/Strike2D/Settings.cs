// Place where settings are stored

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;
using Microsoft.Xna.Framework.Input;

namespace Strike2D
{
    public static class Settings
    {
        private const string SETTINGS_FILE_NAME = "settings.cfg";

        #region GRAPHICS

        public static int ScreenX
        {
            get { return settings.ScreenX; }
            private set { settings.ScreenY = value <= 0 ? 1 : value; }
        }

        public static int ScreenY
        {
            get { return settings.ScreenY; }
            private set { settings.ScreenY = value <= 0 ? 1 : value; }
        }

        public enum ScreenMode
        {
            Windowed,
            FullScreenWindowed,
            FullScreen
        }

        public static ScreenMode Mode
        {
            get { return settings.Mode; }
            private set { settings.Mode = value; }
        }

        public static void ChangeWindowType(ScreenMode mode)
        {
            
        }

        #endregion

        #region AUDIO

        public static int MasterVolume
        {
            get { return settings.MasterVolume; }
            private set { settings.MasterVolume = Math.Min(Math.Max(100, value), value); }
        }

        public static int MusicVolume
        {
            get { return settings.MusicVolume; }
            private set { settings.MusicVolume = Math.Min(Math.Max(100, value), value); }
        }

        public static int EffectVolume
        {
            get { return settings.EffectVolume; }
            private set { settings.EffectVolume = Math.Min(Math.Max(100, value), value); }
        }

        #endregion

        private static SerializableSettings settings = new SerializableSettings();

        public static void SaveConfig()
        {
            FieldInfo[] fields = settings.GetType().GetFields();

            StreamWriter writer;
            try
            {
                File.Delete("settings.cfg");
                Debug.WriteLineVerbose("Settings cleared.");
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLineVerbose("Settings not found.", Debug.DebugType.Warning);
            }
            finally
            {
                Console.Write("Creating Settings File...");
                writer = File.CreateText("settings.cfg");

                foreach (FieldInfo field in fields)
                {
                    switch (field.Name)
                    {
                        case "KeySettings":
                            foreach (string value in settings.KeySettings.Map.Keys)
                            {
                                writer.WriteLine(value + " = " + settings.KeySettings.Map[value]);
                            }
                            break;
                        default:
                            writer.WriteLine(field.Name + " = " + field.GetValue(settings));
                            break;
                    }
                }

                writer.Close();
                Console.Write(" Done. \n");
            }
        }

        public static void LoadConfig()
        {
            if (File.Exists("settings.cfg"))
            {
                FieldInfo[] fields = settings.GetType().GetFields();

                StreamReader reader = File.OpenText("settings.cfg");

                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(' ');

                    // Should be "key = value"
                    if (line.Length != 3) continue;
                    if (line[1] != "=") continue;

                    FieldInfo field = fields.First(f => f.Name == line[0]);

                    // If the setting in the file doesn't exist as a real setting
                    if (field == null) continue;
                    
                    // If the field is a enum
                    if (field.FieldType == typeof(Enum))
                    {
                        try
                        {
                            // If the key-value pair is a keybind
                            if (Enum.IsDefined(typeof(Keys), line[2]) &&
                                settings.KeySettings.Map.ContainsKey(line[0]))
                            {
                                try
                                {
                                    settings.KeySettings.ModifyKey(line[0],
                                        (Keys) Enum.Parse(typeof(Keys), line[2], false));
                                    Debug.WriteLineVerbose(
                                        "Key binding for \"" + line[0] + "\" with Keys." + line[2]);
                                }
                                catch (KeyNotFoundException e)
                                {
                                    Debug.WriteLineVerbose(
                                        "Key " + "\"" + line[2] + "\"" + " is not a valid keybind",
                                        Debug.DebugType.Warning);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine();
                            throw;
                        }
                    }
                    var value = Cast(line[2], field.FieldType);

                    Debug.WriteLineVerbose("Writing to " + field.Name + " with value " +
                                           value.ToString());

                    field.SetValue(settings, value);
                }
            }
            else
            {
                // Save using default settings
                SaveConfig();
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
                Debug.WriteLineVerbose("Unable to cast value to target type", Debug.DebugType.Warning);
                value = null;
            }
            return value;
        }
    }

    /// <summary>
    /// Container for storing all possible settings
    /// </summary>
    internal class SerializableSettings
    {
        public int ScreenX = 1366;
        public int ScreenY = 768;

        public Settings.ScreenMode Mode = Settings.ScreenMode.Windowed;

        public int MasterVolume = 100;
        public int MusicVolume = 100;
        public int EffectVolume = 100;
        public int VoiceVolume = 100;
        
        public KeyMapSettings KeySettings = new KeyMapSettings();
    }

    internal class KeyMapSettings
    {
        public Dictionary<string, Keys> Map { get; private set; }

        public KeyMapSettings()
        {
            Map = new Dictionary<string, Keys>
            {
                // Movement
                {"up", Keys.W},
                {"down", Keys.S},
                {"left", Keys.A},
                {"right", Keys.D},
                {"walk", Keys.LeftShift},

                // Weapons
                {"switchPrimary", Keys.D1},
                {"switchSecondary", Keys.D2},
                {"switchKnife", Keys.D3},
                {"switchGrenade", Keys.D4},
                {"switchBomb", Keys.D5},

                // UI
                {"talkAll", Keys.Y},
                {"talkTeam", Keys.U},
                {"console", Keys.OemTilde},
                {"scoreboard", Keys.Tab},
                {"buy", Keys.B},
                {"buyOther", Keys.O},
                {"noclip", Keys.V},
                
                // Misc
                {"spray", Keys.T}
            };
        }

        public void ModifyKey(string key, Keys value)
        {
            if (Map.ContainsKey(key))
            {
                Map[key] = value;
            }
            else
            {
                Map.Add(key, value);
            }
        }
    }
}