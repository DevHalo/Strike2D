// Place where settings are stored

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework.Input;

namespace Strike2D
{
    public static class Settings
    {
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
                Console.WriteLine("Settings cleared.");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Settings not found. Creating one.");
            }
            finally
            {
                writer = File.CreateText("settings.cfg");

                foreach (FieldInfo field in fields)
                {
                    switch (field.Name)
                    {
                            case "Map":
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
            }
        }

        public static void LoadConfig()
        {
            if (File.Exists("settings.cfg"))
            {
                
            }
            else
            {
                // Save using default settings
                SaveConfig();
            }
        }
    }

    /// <summary>
    /// Container for storing all possible settings
    /// </summary>
    internal class SerializableSettings
    {
        public int ScreenX = 1366;
        public int ScreenY = 768;

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
                {"noclip", Keys.V}
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