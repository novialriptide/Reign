using System;

namespace Reign.Engine.MathExtended
{
    public static class MathFl
    {
        public static float RadiansToDegrees(float radians) => 180 / (float)Math.PI * radians;
        public static float DegreesToRadians(float degrees) => (float)Math.PI / 180 * degrees;
        /// <summary>
        /// Moves a float toward the target.
        /// </summary>
        /// <returns>The updated value.</returns>
        public static float MoveTowards(this float current, float target, float maxDistanceDelta)
        {
            if (MathF.Abs(target - current) <= maxDistanceDelta)
                return target;

            return current + MathF.Sign(target - current) * maxDistanceDelta;
        }
    }
}
