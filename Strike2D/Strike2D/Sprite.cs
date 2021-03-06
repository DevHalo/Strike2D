﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public class Sprite : GameObject
    {
        public Color SpriteColour = Color.White;
        public string AssetKey { get; private set; }
        public float Alpha { get; private set; }
        private Texture2D sprite;
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

            sprite = (Texture2D)AssetManager.GetAsset(assetKey);
            
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
            
            sb.Draw(sprite, Position, SpriteColour * Alpha);
        }
    }
}