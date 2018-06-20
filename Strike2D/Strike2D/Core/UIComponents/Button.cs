// Author: Mark Voong
// File Name: Button.cs
// Project Name: Global Offensive
// Creation Date: Dec 23rd, 2015
// Modified Date: Sept 14th, 2017
// Description: Creates a button, which can be clicked

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D.Core.UIComponents
{
    public sealed class Button : GUIComponent
    {
        /// <summary>
        /// The current state of the button
        /// </summary>
        public override State CurState { get; protected set; }

        /// <summary>
        /// Used to identify the button
        /// </summary>
        public override string Identifier { get; protected set; }

        private Vector2 startPosition;                  // The off-screen position of the button
        private Vector2 endPosition;                    // The final destination of the button
        private Vector2 textPosition;                   // Position of the text
        private Color fillColour;                       // The colour used for the fill of the button
        private Color borderColour;                     // The colour used for the border of the button
        private Color textColour;                       // The colour used for the text
        private string text;                            // The text shown inside the button
        private float alpha = 0.0f;                     // The alpha transparency of the button
        private float changeRate;
        private float animTime;                         // Time the button takes to move from one point to another
        private EasingFunctions.AnimationType animType; // Type of animation the button should use

        private Texture2D pixelTexture;                 // Stores the pixel texture
        private SpriteFont defaultFont;                 // Stores the default font

        private bool debug = false;                     // Shows borders of buttons if true

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="dimensions"></param>
        /// <param name="textColour"></param>
        /// <param name="text"></param>
        /// <param name="animTime"></param>
        /// <param name="animType"></param>
        /// <param name="animDir"></param>
        public Button(string identifier, Rectangle dimensions, Color textColour, string text, float animTime,
            EasingFunctions.AnimationType animType, AnimationDirection animDir)
        {
            Identifier = identifier;
            this.textColour = textColour;
            this.text = text;
            this.animTime = animTime;
            this.animType = animType;
            changeRate = 1f / animTime;
            CurState = State.InActive;
            endPosition = new Vector2(Dimensions.X, Dimensions.Y);
            startPosition = SetStartPosition(animDir);
            Dimensions.Location = new Point((int)startPosition.X, (int)startPosition.Y);
            Dimensions.Width = dimensions.Width;
            Dimensions.Height = dimensions.Height;

            pixelTexture = (Texture2D) AssetManager.GetAsset("pixelTexture");
            defaultFont = UIManager.GetDefaultFont();
        }
        
        /// <summary>
        /// Creates a button with a different border and fill colour
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="dimensions"></param>
        /// <param name="fillColour"></param>
        /// <param name="borderColour"></param>
        /// <param name="textColour"></param>
        /// <param name="text"></param>
        /// <param name="animTime"></param>
        /// <param name="animType"></param>
        /// <param name="animDir"></param>
        public Button(string identifier, Rectangle dimensions, Color fillColour, Color borderColour, Color textColour,
            string text, float animTime, EasingFunctions.AnimationType animType, AnimationDirection animDir) :
            this(identifier, dimensions, textColour, text, animTime, animType, animDir)
        {
            this.fillColour = fillColour;
            this.borderColour = borderColour;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animDir"></param>
        /// <returns></returns>
        public Vector2 SetStartPosition(AnimationDirection animDir)
        {
            switch (animDir)
            {
                case AnimationDirection.Left:
                    return new Vector2(1366, endPosition.Y);
                case AnimationDirection.Right:
                    return new Vector2(-250, endPosition.Y);
                case AnimationDirection.Up:
                    return new Vector2(endPosition.X, 768);
                case AnimationDirection.Down:
                    return new Vector2(endPosition.X, -250);
                case AnimationDirection.None:
                    return endPosition;
            }
            return Vector2.Zero;
        }

        /// <summary>
        /// Updates all logic for the button
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(float gameTime)
        {
            switch (CurState)
            {
                case State.TransitionIn:

                    // Advance time
                    Timer += gameTime;

                    // Move the button
                    Dimensions.X =
                        (int) EasingFunctions.Animate(Timer, startPosition.X, endPosition.X, animTime, animType);
                    Dimensions.Y =
                        (int) EasingFunctions.Animate(Timer, startPosition.Y, endPosition.Y, animTime, animType);
                    
                    // If the button has reached its end point, set it to active
                    if (Timer >= animTime)
                    {
                        CurState = State.Active;
                    }

                    // Change the alpha
                    if (alpha <= 1.0f)
                    {
                        alpha += changeRate * gameTime;
                    }
                    break;
                case State.TransitionOut:

                    // Advance time
                    Timer -= gameTime;

                    // Move the button
                    Dimensions.X =
                        (int)EasingFunctions.Animate(Timer, startPosition.X, endPosition.X, animTime, animType);
                    Dimensions.Y =
                        (int)EasingFunctions.Animate(Timer, startPosition.Y, endPosition.Y, animTime, animType);

                    // If the button has reached its end point, set it to active
                    if (Timer <= 0.0f)
                    {
                        CurState = State.InActive;
                    }

                    // Change the alpha
                    if (alpha >= 0.0f)
                    {
                        alpha -= changeRate * gameTime;
                    }
                    break;
            }
        }

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="assets"></param>
        public override void Draw(SpriteBatch sb)
        {
            if (CurState != State.InActive)
            {
                // Draw fill
                sb.Draw(pixelTexture, Dimensions, fillColour);

                // Draw text
                Vector2 centeredText = new Vector2(
                    Dimensions.Center.X - (defaultFont.MeasureString(text).X / 2),
                    Dimensions.Center.Y - (defaultFont.MeasureString(text).Y / 2));

                sb.DrawString(defaultFont, text, centeredText, textColour * alpha, 0, Vector2.Zero, 1f,
                    SpriteEffects.None, 0);

                // Draw border

                if (debug)
                {
                    // Draw left side
                    sb.Draw(pixelTexture, new Rectangle(Dimensions.Left, Dimensions.Y, 1, Dimensions.Height),
                        Color.Red * alpha);
                    // Draw right side
                    sb.Draw(pixelTexture, new Rectangle(Dimensions.Right, Dimensions.Y, 1, Dimensions.Height),
                        Color.Red * alpha);
                    // Draw bottom side
                    sb.Draw(pixelTexture, new Rectangle(Dimensions.X, Dimensions.Bottom, Dimensions.Width, 1),
                        Color.Red * alpha);
                    // Draw top side
                    sb.Draw(pixelTexture, new Rectangle(Dimensions.X, Dimensions.Y, Dimensions.Width, 1),
                        Color.Red * alpha);
                }
                else
                {
                    // Draw left side
                    sb.Draw(pixelTexture, new Rectangle(Dimensions.Left, Dimensions.Y, 1, Dimensions.Height),
                        borderColour * alpha);
                    // Draw right side
                    sb.Draw(pixelTexture, new Rectangle(Dimensions.Right, Dimensions.Y, 1, Dimensions.Height),
                        borderColour * alpha);
                    // Draw bottom side
                    sb.Draw(pixelTexture, new Rectangle(Dimensions.X, Dimensions.Bottom, Dimensions.Width, 1),
                        borderColour * alpha);
                    // Draw top side
                    sb.Draw(pixelTexture, new Rectangle(Dimensions.X, Dimensions.Y, Dimensions.Width, 1),
                        borderColour * alpha);
                }
            }
        }

        /// <summary>
        /// Checks if the button is hovered over
        /// </summary>
        /// <returns></returns>
        public bool Hover(InputManager input)
        {
            return CurState == State.Active && Dimensions.Contains((int) input.MousePosition.X, (int) input.MousePosition.Y);
        }

        /// <summary>
        /// Check if the button was clicked
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Clicked(InputManager input)
        {
            if (CurState == State.Active)
            {
                return Hover(input) && input.LeftClick();
            }
            return false;
        }
    }
}