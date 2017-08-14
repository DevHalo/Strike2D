using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Strike2D
{
    public abstract class GameObject
    {
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public bool Active { get; protected set; } = true;
        public bool Render { get; protected set; } = true;

        /// <summary>
        /// Returns the position of the game object as a point
        /// </summary>
        /// <returns></returns>
        public Point VecToPoint()
        {
            return new Point((int)Position.X, (int)Position.Y);
        }
        
        public abstract void Update(float gameTime);
        public abstract void Draw(SpriteBatch sb);
    }
}