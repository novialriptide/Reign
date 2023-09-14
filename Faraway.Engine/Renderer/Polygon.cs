using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Vector2 = System.Numerics.Vector2;

namespace Faraway.Engine.Renderer
{
    public class Polygon : Shape
    {
        public List<Vector2> Vertices = new List<Vector2>();
        public Color Color;
    }
}