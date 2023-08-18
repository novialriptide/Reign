using System;

namespace Faraway.Engine.MathExtended
{
    public static class MathFl
    {
        public static float RadiansToDegrees(float radians) => (180 / (float)Math.PI) * radians;
        public static float DegreesToRadians(float degrees) => ((float)Math.PI / 180) * degrees;
    }
}
