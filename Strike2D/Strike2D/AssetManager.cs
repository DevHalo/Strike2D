using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        public void LoadAsync(LoadType loadType)
        {
            Assets.Clear();
            
            switch (loadType)
            {
                case LoadType.Core:
                    Assets = Task.Run(() => LoadCoreContentAsync()).Result;
                    break;
                case LoadType.Game:
                    Assets = Task.Run(() => LoadGameContentAsync()).Result;
                    break;
            }

            loaded = Assets != null;
         
            if (!loaded) { Debug.WriteLine("FAILED TO LOAD", Debug.DebugType.CriticalError); }
        }

        /// <summary>
        /// Loads the basic assets into the game (UI, Menu Sounds)
        /// </summary>
        private SortedDictionary<string, object> LoadCoreContentAsync()
        {
            try
            {
                Dictionary<string, object> assetsToLoad = new Dictionary<string, object>();

                // Assets

                // Bake the list
                return new SortedDictionary<string, object>(assetsToLoad);
            }
            catch (Exception e)
            {
                Debug.WriteLine("FAILED TO LOAD", Debug.DebugType.CriticalError);
            }
            return null;
        }

        private SortedDictionary<string, object> LoadGameContentAsync()
        {
            try
            {
                Dictionary<string, object> assetsToLoad = new Dictionary<string, object>();
            
                // Assets
                
            
                // Bake the list
                return new SortedDictionary<string, object>(assetsToLoad);
            }
            catch (Exception e)
            {
                Debug.WriteLine("FAILED TO LOAD", Debug.DebugType.CriticalError);
            }
            return null;
        }
    }
}