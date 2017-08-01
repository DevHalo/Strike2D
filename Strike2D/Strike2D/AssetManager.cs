using System;
using System.Collections.Generic;
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
        
        private bool Loaded() { return loaded; }
        private int LoadedAssets() { return loadedAssets; }

        private ContentManager content;
        
        /// <summary>
        /// Type of loading
        /// </summary>
        public enum LoadType
        {
            Core,
            Game
        }

        public AssetManager(ContentManager content)
        {
            this.content = content;
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
                Thread.Sleep(1000);
                Debug.WriteLineVerbose("Loaded Asset 1");
                Thread.Sleep(2000);
                Debug.WriteLineVerbose("Loaded Asset 2");

                // Bake the list
                Assets = new SortedDictionary<string, object>(assetsToLoad);
                loaded = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("FAILED TO LOAD", Debug.DebugType.CriticalError);
            }
        }

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
    }
}