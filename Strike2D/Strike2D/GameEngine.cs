// Handles all game state and flow

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public class GameEngine
    {
        private static InputManager input;          // Handles Input
        private static AssetManager assets;  // Handles asset loading and unloading
        
        // GameObjects
        public List<GameObject> UIObjects = new List<GameObject>();
        public List<GameObject> IngameObjects = new List<GameObject>();
        
        private State curState;
        
        public enum State
        {
            Splash,        // Startup
            Menu,          // Menu Screen
            Loading,       // Loading Screen
            Connecting,    // Connecting to Server Screen
            Game           // Connected to game via dedicated or listen
        }

        /// <summary>
        /// The current state of the game
        /// </summary>
        public State CurState
        {
            get { return curState; }
            set
            {
                Debug.WriteLineVerbose("State Change: " + curState + " -> " + value);
                curState = value;
            }
        }
        
        /// <summary>
        /// Gets the asset from the asset manager using a key
        /// </summary>
        /// <param name="key"> String key used to reference the asset</param>
        /// <returns></returns>
        public static object GetAsset(string key)
        {
            if (assets != null)
            {
                if (assets.Assets.ContainsKey(key))
                {
                    return assets.Assets[key];
                }
            }
            
            Debug.WriteLineVerbose("Attempting to access asset when the asset manager has not been initialized!",
                Debug.DebugType.CriticalError);
            
            return null;
        }

        public static InputManager Input => input;

        /// <summary>
        /// Run at startup. Initializes all the required manager classes
        /// </summary>
        /// <param name="main"></param>
        public void Init(Strike2D main)
        {
            input = new InputManager();
            assets = new AssetManager(main);
            Debug.WriteLineVerbose("Ready to Go. Welcome to Strike 2D " + Manifest.Version);
            CurState = State.Splash;
        }

        /// <summary>
        /// Main update loop for the game
        /// </summary>
        public void Update()
        {
            input.Tick();

            switch (CurState)
            {
                case State.Splash:
                    CurState = State.Loading;
                    assets.Load(AssetManager.LoadType.Core);
                    break;
                case State.Loading:
                    if (assets.Loaded())
                    {
                        CurState = State.Menu;
                    }
                    break;
                case State.Connecting:
                    break;
            }
            
            input.Tock();
        }

        /// <summary>
        /// Main draw loop for the game
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            foreach (GameObject obj in IngameObjects)
            {
                obj.Draw(sb);
            }

            foreach (GameObject obj in UIObjects)
            {
                obj.Draw(sb);
            }
        }
    }
}