using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Strike2D
{
    public class AssetManager
    {
        public SortedDictionary<string, object> Assets { get; private set; }

        public bool AssetsLoaded()
        {
            return Assets == null;
        }

        /// <summary>
        /// Loads the basic assets into the game (UI, Menu Sounds)
        /// </summary>
        public void LoadCoreContent()
        {
            Assets = null;

            Dictionary<string, object> assetsToLoad = new Dictionary<string, object>();
            
            // Assets
            
            // Bake the list
            Assets = new SortedDictionary<string, object>(assetsToLoad);
        }

        public void LoadGameContent()
        {
            Assets = null;

            Dictionary<string, object> assetsToLoad = new Dictionary<string, object>();
            
            // Assets
            
            // Bake the list
            Assets = new SortedDictionary<string, object>(assetsToLoad);
        }
    }
}