using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Strike2D
{
    public class Strike2DMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Strike2DMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadBase();
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

            graphics.IsFullScreen = Settings.FullScreen;
            Window.AllowUserResizing = true;

            Window.Title = "Strike 2D - " + Manifest.Version + " - " + Manifest.Environment + " Build";
            
            graphics.ApplyChanges();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == 
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                    Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}