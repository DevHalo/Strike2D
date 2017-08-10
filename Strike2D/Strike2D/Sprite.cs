using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public class Sprite : GameObject
    {
        public Color SpriteColour;
        public string AssetKey { get; private set; }
        public float Alpha;
        private Texture2D sprite;
        private float fadeRate;

        public enum AnimationState
        {
            Start,
            Animating,
            Pause,
            End
        }
        
        public AnimationState CurState { get; private set; }
        
        public bool Fade { get; private set; }
        
        public Sprite(bool startActive, string assetKey, float fadeRate = 0.2f)
        {
            Alpha = startActive ? 1.0f : 0.0f;
            this.fadeRate = fadeRate;

            sprite = (Texture2D)GameEngine.GetAsset(assetKey);
            
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, sprite.Width, sprite.Height);
        }

        /// <summary>
        /// Changes whether the sprite should fade in or out
        /// </summary>
        /// <param name="direction"> True to fade in, False to fade out</param>
        public void ChangeFade(bool direction)
        {
            if (direction) { fadeRate = Math.Abs(fadeRate); }
            else if (fadeRate > 0) { fadeRate = -fadeRate; }
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
                Alpha += fadeRate;
                Alpha = MathHelper.Clamp(Alpha, 0.0f, 1.0f);
            }

            SpriteColour.A = (byte)(255 * Alpha);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (!Render) { return; }
            
            sb.Draw(sprite, Position, SpriteColour);
        }
    }
}