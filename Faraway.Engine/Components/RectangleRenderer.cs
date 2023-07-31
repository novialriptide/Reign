using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = System.Numerics.Vector2;

namespace Faraway.Engine.Components
{
    /// <summary>
    /// This is just a temporary renderer until `ShapeRenderer` gets finalized.
    /// </summary>
    public sealed class RectangleRenderer : Component
    {
        public Texture2D Texture;
        public Vector2 Offset = Vector2.Zero;
        public Vector2 Size = Vector2.One;

        public Color Color = new Color(255, 255, 255, 255);
    }
}
