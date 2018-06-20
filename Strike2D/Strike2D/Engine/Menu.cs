// Handles all menu input and drawing

using CSCore.SoundOut;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Strike2D.Core;

namespace Strike2D.Engine
{
    public class Menu
    {
        private Sprite[] backgrounds;
        private int selectedBackground;
        private SoundContainer menuTheme;
        private GameEngine engine;
        private Sprite logo;

        private float animationTime;
        private Rectangle logoScissor;
        private Vector2 endPos;

        public enum MenuState
        {
            TransitionIn,
            Main,
            Options,
            Lobby,
            TransitionOut
        }

        public Menu(GameEngine instance)
        {
            backgrounds = new[]
            {
                new Sprite(false, "ct_background", 0.4f),
                new Sprite(false, "t_background", 0.4f),
            };
            
            logo = new Sprite(false, "logo_full", 1f);
            
            endPos = new Vector2(GameEngine.Center().X - logo.Texture.Width / 2f, (Settings.ScreenY * 0.50f));
            
            logo.Position = new Vector2(GameEngine.Center().X, endPos.Y);

            selectedBackground = GameEngine.RandomGenerator.Next(2);

            menuTheme = (SoundContainer) AssetManager.GetAsset("theme");

            engine = instance;
        }

        private MenuState menuState = MenuState.TransitionIn;

        public void PlayMenuMusic()
        {
            menuTheme.Play(Settings.MusicVolume);
        }

        public void Update(float gameTime)
        {
            switch (menuState)
            {
                case MenuState.TransitionIn:

                    if (backgrounds[selectedBackground].Alpha >= 1.0f)
                    {
                        menuState = MenuState.Main;
                        logo.ChangeFade(true);
                    }
                    else
                    {
                        if (!backgrounds[selectedBackground].Fade)
                        {
                            backgrounds[selectedBackground].ChangeFade(true);
                        }
                    }

                    break;
                case MenuState.Main:
                    animationTime += gameTime;
                    logo.Position = new Vector2(
                        (float)EasingFunctions.Animate(animationTime, GameEngine.Center().X, endPos.X, 2.0f, 
                            EasingFunctions.AnimationType.QuinticOut),
                        logo.Position.Y
                    );
                    break;
                case MenuState.Options:
                    break;
                case MenuState.Lobby:
                    break;
                case MenuState.TransitionOut:
                    break;
            }

            if (engine.Input.Tapped(Keys.Escape))
            {
                engine.Exit();
            }

            if (menuTheme.PlayState() == PlaybackState.Stopped)
            {
                menuTheme.Play(Settings.MusicVolume);
            }

            backgrounds[selectedBackground].Update(gameTime);
            logo.Update(gameTime);
        }

        public void Draw(SpriteBatch sb)
        {
            backgrounds[selectedBackground].Draw(sb);
            
            logoScissor = new Rectangle(
                (int)EasingFunctions.Animate(animationTime, GameEngine.Center().X, endPos.X, 2.0f, EasingFunctions.AnimationType.QuinticOut),
                (int)logo.Position.Y,
                (int)EasingFunctions.Animate(animationTime, 0, logo.Texture.Width, 2.0f, EasingFunctions.AnimationType.QuinticOut),
                logo.Texture.Height
            );

            Rectangle oldRect = sb.GraphicsDevice.ScissorRectangle;
            sb.GraphicsDevice.ScissorRectangle = logoScissor;
            
            logo.Draw(sb);

            sb.GraphicsDevice.ScissorRectangle = oldRect;
        }
    }
}