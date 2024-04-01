using System.Numerics;

namespace Reign.Engine.MathExtended
{
    public static class Collisions
    {
        public static bool RectToRect(Vector2 position1, Vector2 size1, Vector2 position2, Vector2 size2)
        {
            return position1.X + size1.X >= position2.X &&
                position1.X <= position2.X + size2.X &&
                position1.Y + size1.Y >= position2.Y &&
                position1.Y <= position2.Y + size2.Y;
        }
    }
}
