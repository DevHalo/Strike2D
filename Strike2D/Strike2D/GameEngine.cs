// Handles all game state and flow

using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public class GameEngine
    {
        private InputManager input;        // Handles Input
        private static AssetManager assets;       // Handles asset loading and unloading
        
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
        
        private State curState;
 
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
            
        }

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
    }
}