using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public class AssetManager
    {
        public SortedDictionary<string, object> Assets { get; private set; }
        private volatile bool loaded = false;
        private volatile int loadedAssets = 0;
        
        public bool Loaded() { return loaded; }
        public int LoadedAssets() { return loadedAssets; }

        public static string RootDirectory = "Content/";

        private Strike2D main;
        
        public LoadType LoadedType { get; private set; } = LoadType.Unloaded;
        
        /// <summary>
        /// Type of loading
        /// </summary>
        public enum LoadType
        {
            Core,
            Game,
            Unloaded
        }

        public AssetManager(Strike2D main)
        {
            this.main = main;
        }
        
        /// <summary>
        /// Loads assets into the game on a separate thread
        /// </summary>
        /// <param name="loadType"></param>
        public void Load(LoadType loadType)
        {
            loaded = false;
            Assets = new SortedDictionary<string, object>();
            
            Debug.WriteLineVerbose("Loading Assets");
            
            switch (loadType)
            {
                case LoadType.Core:
                    Thread bgLoad = new Thread(LoadCoreContent);
                    bgLoad.IsBackground = true;
                    bgLoad.Start();
                    break;
                case LoadType.Game:
                    LoadGameContent();
                    break;
            }

            if (!loaded) { Debug.WriteLine("FAILED TO LOAD", Debug.DebugType.CriticalError); }
            LoadedType = loaded ? loadType : LoadType.Unloaded;
        }

        /// <summary>
        /// Loads the basic assets into the game (UI, Menu Sounds)
        /// </summary>
        private void LoadCoreContent()
        {
            try
            {
                Dictionary<string, object> assetsToLoad = new Dictionary<string, object>();

                // Assets
                assetsToLoad.Add("t_background", Load<Texture2D>("Materials/Background/t_background.png"));
                
                // Bake the list
                Assets = new SortedDictionary<string, object>(assetsToLoad);
                loaded = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("FAILED TO LOAD", Debug.DebugType.CriticalError);
            }
        }

        /// <summary>
        /// Loads game assets such as sprites and sounds
        /// </summary>
        private void LoadGameContent()
        {
            try
            {
                Dictionary<string, object> assetsToLoad = new Dictionary<string, object>();
            
                // Assets

                // Bake the list
            }
            catch (Exception e)
            {
                Debug.WriteLine("FAILED TO LOAD", Debug.DebugType.CriticalError);
            }
        }

        /// <summary>
        /// Loads a map from the maps directory
        /// </summary>
        /// <param name="mapName"> the name of the map</param>
        /// <param name="downloaded"> Was this map downloaded from a server</param>
        public void LoadMap(string mapName, bool downloaded)
        {
            // If the maps folder doesn't exist, make one
            if (!File.Exists(RootDirectory + "Maps"))
            {
                
            }
        }

        /// <summary>
        /// Loads a type from the RootDirectory folder
        /// </summary>
        /// <param name="fileName"> Filename including extension. Structure is relative to RootDirectory</param>
        /// <typeparam name="T"> Type you want to load</typeparam>
        /// <returns></returns>
        private object Load<T>(string fileName)
        {
            Type t = typeof(T);
            object result = null;
            
            Debug.WriteLineVerbose("Loading " + fileName + " type of " + t);

            if (!File.Exists(RootDirectory + fileName))
            {
                Debug.WriteLineVerbose("File \"" + fileName + " does not exist!", Debug.DebugType.CriticalError);
                return default(T);
            }
            
            if (t == typeof(Texture2D))
            {
                try
                {
                    FileStream fileStream = new FileStream(RootDirectory + fileName, FileMode.Open);
                    result = Texture2D.FromStream(main.GraphicsDevice, fileStream);
                    fileStream.Dispose();
                }
                catch (Exception e)
                {
                    Debug.WriteLineVerbose("Failed to load Texture2D \"" + fileName + "\"",
                        Debug.DebugType.CriticalError);
                }
            }

            if (result == null)
            {
                Debug.WriteLineVerbose("Failed to load asset \"" + fileName + "\"" + "!", 
                    Debug.DebugType.CriticalError);
            }
            return result;
        }
    }
}