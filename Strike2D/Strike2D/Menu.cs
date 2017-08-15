// Handles all menu input and drawing

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Strike2D
{
    public class Menu
    {
        private Sprite[] backgrounds;
        private int selectedBackground;
        private SoundContainer menuTheme;
        private GameEngine engine;
        
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

            selectedBackground = GameEngine.RandomGenerator.Next(2);

            menuTheme = (SoundContainer)AssetManager.GetAsset("theme");

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
            
            backgrounds[selectedBackground].Update(gameTime);
        }

        public void Draw(SpriteBatch sb)
        {
            backgrounds[selectedBackground].Draw(sb);
        }
    }
}