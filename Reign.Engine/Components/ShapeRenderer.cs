using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Reign.Engine.Components
{
    /// <summary>
    /// Work-in-progress
    /// </summary>
    internal sealed class ShapeRenderer : Component
    {
        public bool Loop;
        public Vector2[] Vertices;
        public Color BorderColor;
        public Color FillColor;
        public Vector2 Offset;
    }
}
