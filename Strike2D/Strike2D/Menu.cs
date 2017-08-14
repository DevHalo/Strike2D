// Handles all menu input and drawing

using System;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public class Menu
    {
        private Sprite[] backgrounds;
        private int selectedBackground;
        
        public enum MenuState
        {
            TransitionIn,
            Main,
            Options,
            Lobby,
            TransitionOut
        }

        public Menu()
        {
            backgrounds = new[]
            {
                new Sprite(false, "ct_background"),
                new Sprite(false, "t_background"), 
            };

            selectedBackground = GameEngine.RandomGenerator.Next(2);
        }

        private MenuState menuState = MenuState.TransitionIn;

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
            
            backgrounds[selectedBackground].Update(gameTime);
        }

        public void Draw(SpriteBatch sb)
        {
            backgrounds[selectedBackground].Draw(sb);
        }
    }
}