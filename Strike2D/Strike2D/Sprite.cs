using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public class Sprite : GameObject
    {
        public Color Color;
        public string AssetKey { get; private set; }
        public float Alpha;
        private Texture2D sprite;
        
        public Sprite(bool startActive)
        {
            Alpha = startActive ? 1.0f : 0.0f;
            
            
        }

        public override void Update(float gameTime)
        {
            
        }

        public override void Draw(SpriteBatch sb)
        {
            
        }
    }
}