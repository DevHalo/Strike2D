﻿// Handles all game state and flow

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public class GameEngine
    {
        // Manager Classes
        private static InputManager input;      // Handles Input
        private static AssetManager assets;     // Handles asset loading and unloading
        private static AudioManager audio;      // Handles audio

        // Accessors
        public InputManager Input => input;
        public AssetManager Assets => assets;
        public AudioManager Audio => audio;

        // RNG
        public static Random RandomGenerator { get; private set; }   // Used for random numbers
        
        // GameObjects
        public List<GameObject> UIObjects = new List<GameObject>();
        public List<GameObject> IngameObjects = new List<GameObject>();
        
        // The current state
        private State curState;
        
        // Screens
        private Menu menuManager;

        /// <summary>
        /// Returns the center coordinate of the screen
        /// </summary>
        /// <returns></returns>
        public static Vector2 Center()
        {
            return new Vector2(Settings.ScreenX / 2f, Settings.ScreenY / 2f);
        }
        
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
        /// Run at startup. Initializes all the required manager classes
        /// </summary>
        /// <param name="main"></param>
        public void Init(Strike2D main)
        {
            audio = new AudioManager();
            input = new InputManager();
            assets = new AssetManager(main);
            Debug.WriteLineVerbose("Ready to Go. Welcome to Strike 2D " + Manifest.Version);
            CurState = State.Splash;
            
            RandomGenerator = new Random();
            
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        public void Exit()
        {
            Application.Exit();
        }

        /// <summary>
        /// Main update loop for the game
        /// </summary>
        public void Update(float gameTime)
        {
            input.Tick();

            switch (CurState)
            {
                case State.Splash:
                    CurState = State.Loading;
                    assets.Load(AssetManager.LoadType.Core);
                    break;
                case State.Loading:
                    if (AssetManager.Loaded())
                    {
                        menuManager = new Menu(this);
                        menuManager.PlayMenuMusic();
                        CurState = State.Menu;
                    }
                    break;
                case State.Menu:
                    menuManager.Update(gameTime);
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
            sb.Begin();
            
            switch (curState)
            {
                case State.Splash:
                    break;
                case State.Menu:
                    menuManager.Draw(sb);
                    break;
                case State.Loading:
                    break;
                case State.Connecting:
                    break;
                case State.Game:
                    foreach (GameObject obj in IngameObjects)
                    {
                        obj.Draw(sb);
                    }
                    
                    menuManager.Draw(sb);
                    break;
            }

            foreach (GameObject obj in UIObjects)
            {
                obj.Draw(sb);
            }
            
            sb.End();
        }
    }
}