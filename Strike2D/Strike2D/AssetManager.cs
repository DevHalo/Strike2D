using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Strike2D
{
    public class AssetManager
    {
        public SortedDictionary<string, object> Assets { get; private set; }
        private volatile bool loaded = false;
        private volatile int loadedAssets = 0;
        
        private bool Loaded() { return loaded; }
        private int LoadedAssets() { return loadedAssets; }
        
        /// <summary>
        /// Type of loading
        /// </summary>
        public enum LoadType
        {
            Core,
            Game
        }
        
        public async void LoadAsync(LoadType loadType)
        {
            Assets.Clear();
            
            switch (loadType)
            {
                case LoadType.Core:
                    Assets = await LoadCoreContentAsync();
                    break;
                case LoadType.Game:
                    Assets = await LoadGameContentAsync();
                    break;
            }
        }

        /// <summary>
        /// Loads the basic assets into the game (UI, Menu Sounds)
        /// </summary>
        private async Task<SortedDictionary<string, object>> LoadCoreContentAsync()
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

        private Task LoadGameContentAsync()
        {
            try
            {
                Dictionary<string, object> assetsToLoad = new Dictionary<string, object>();
            
                // Assets
            
                // Bake the list
                Assets = new SortedDictionary<string, object>(assetsToLoad);
            }
            catch (Exception e)
            {
                Debug.WriteLine("FAILED TO LOAD", Debug.DebugType.CriticalError);
            }
        }
    }
}