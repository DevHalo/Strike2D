using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Strike2D
{
    public class Strike2D : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameEngine Engine { get; private set; }

        public Strike2D()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            LoadBase();
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        
        /// <summary>
        /// Loads the config file for basic settings and
        /// assigns the correct variables
        /// </summary>
        private void LoadBase()
        {
            Settings.LoadConfig();
            
            // Sets the screen up
            
            graphics.PreferredBackBufferWidth = Settings.ScreenX;
            graphics.PreferredBackBufferHeight = Settings.ScreenY;
            
            Window.Position = new Point(
                graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2 - graphics.PreferredBackBufferWidth / 2,
                graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2 - graphics.PreferredBackBufferHeight / 2);


            switch (Settings.Mode)
            {
                case Settings.ScreenMode.FullScreenWindowed:
                    // temp fix until #5585 is pushed
                    /*
                    graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.WorkingArea.Width;
                    graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.WorkingArea.Height;
                    Window.IsBorderless = true;
                    Window.Position = Point.Zero;
                    */
                case Settings.ScreenMode.FullScreen:
                    graphics.HardwareModeSwitch = false;
                    graphics.ToggleFullScreen();
                    break;
            }
            
            Window.Title = "Strike 2D - " + Manifest.Version + " - " + Manifest.Environment + " Build";
            
            graphics.ApplyChanges();
            
            Engine = new GameEngine();
            Engine.Init(this);
        }

        protected override void UnloadContent()
        {
            Engine.Assets.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            float time = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            Engine.Update(time);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (Settings.RenderOffScreen)
            {
                Engine.Draw(spriteBatch);
            }
            base.Draw(gameTime);
        }
    }
}