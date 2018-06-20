using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Strike2D.Engine;

namespace Strike2D.Core
{
    public class Sprite : GameObject
    {
        public Color SpriteColour = Color.White;
        public string AssetKey { get; private set; }
        public float Alpha { get; private set; }
        public Texture2D Texture { get; private set; }
        private float fadeRate;

        public enum AnimationState
        {
            Start,
            Animating,
            Pause,
            End
        }
        
        /// <summary>
        /// Gets the current animation state
        /// </summary>
        public AnimationState CurState { get; private set; }
        
        /// <summary>
        /// Is the sprite currently fading in or out
        /// </summary>
        public bool Fade { get; private set; }
        
        public Sprite(bool startActive, string assetKey, float fadeRate = 0.2f)
        {
            Alpha = startActive ? 1.0f : 0.0f;
            this.fadeRate = fadeRate;

            Texture = (Texture2D)AssetManager.GetAsset(assetKey);
            
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        /// <summary>
        /// Changes whether the sprite should fade in or out
        /// </summary>
        /// <param name="direction"> True to fade in, False to fade out</param>
        public void ChangeFade(bool direction)
        {
            if (direction) { fadeRate = Math.Abs(fadeRate); }
            else if (fadeRate > 0) { fadeRate = -fadeRate; }

            Fade = true;
        }

        public override void Update(float gameTime)
        {
            if (!Active) { return; }

            switch (CurState)
            {
                case AnimationState.Start:
                    break;
                case AnimationState.Animating:
                    break;
                case AnimationState.Pause:
                    break;
                case AnimationState.End:
                    break;
            }

            if (Fade)
            {
                Alpha += fadeRate * gameTime;
                Alpha = MathHelper.Clamp(Alpha, 0.0f, 1.0f);

                if (Alpha <= 0.0f || Alpha >= 1.0f) { Fade = false; }
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (!Render) { return; }
            
            sb.Draw(Texture, Position, SpriteColour * Alpha);
        }

        /// <summary>
        /// Draws the sprite stretched to fill the screen
        /// </summary>
        /// <param name="sb"></param>
        public void DrawFillScreen(SpriteBatch sb)
        {
            if (!Render) { return; }
            
            float xRatio = (float)Settings.ScreenX / Texture.Width;
            float yRatio = (float) Settings.ScreenY / Texture.Height;
            
            sb.Draw(Texture, Position, null, (SpriteColour * Alpha), 0f, Vector2.Zero, new Vector2(xRatio, yRatio),
                SpriteEffects.None, 0f);
            
        }
    }
}