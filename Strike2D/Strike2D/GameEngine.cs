// Handles all game state and flow

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public class GameEngine
    {
        private InputManager input;
        private AssetManager assets;
        
        public enum State
        {
            Splash,
            Menu,
            Loading,
            Game
        }
 
        public void Init(Strike2D main)
        {
            input = new InputManager();
            assets = new AssetManager(main.Content);
            assets.Load(AssetManager.LoadType.Core);
            Debug.WriteLineVerbose("Ready to Go. Welcome to Strike 2D " + Manifest.Version);
        }


        public void Update()
        {
            input.Tick();
            
            input.Tock();
        }

        public void Draw(SpriteBatch sb)
        {
            
        }
    }
}