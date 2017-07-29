using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public abstract class GameObject
    {
        public Vector2 Position { get; protected set; }
        public bool Active { get; protected set; }
        public bool Render { get; protected set; }
        
        public abstract void Update(float gameTime);
        public abstract void Draw(SpriteBatch sb);
    }
}