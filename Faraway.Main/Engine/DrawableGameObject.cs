using Faraway.Main.Engine.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Main.Engine
{
    /// <summary>
    /// Derived from <c>GameObject</c>, always has the components <c>Transform</c> and <c>Sprite2D</c>
    /// </summary>
    public abstract class Drawable2DGameObject : GameObject
    {
        public Transform Transform { get; private set; }
        public Sprite2D Sprite2D { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
