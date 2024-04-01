using System;
using System.Numerics;

namespace Reign.Engine.MathExtended
{
    public static class MathV
    {
        /// <summary>
        /// Gets the magnitude.
        /// </summary>
        public static float GetMagnitude(this Vector2 vector2) => MathF.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y);
        /// <summary>
        /// Sets the magnitude of the specified vector2.
        /// </summary>
        public static Vector2 SetMagnitude(this Vector2 vector2, float magnitude)
        {
            // Referenced from: https://stackoverflow.com/questions/41317291/setting-the-magnitude-of-a-2d-vector
            float ratio = magnitude / GetMagnitude(vector2);
            return vector2 * ratio;
        }
        /// <summary>
        /// Moves a Vector2 toward the target.
        /// </summary>
        /// <returns>The updated position.</returns>
        public static Vector2 MoveTowards(this Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            // Referenced from: https://github.com/pygame/pygame/pull/2929
            var a = target - current;
            float magnitude = GetMagnitude(a);
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
                return target;

            return current + a / magnitude * maxDistanceDelta;
        }
        /// <summary>
        /// Rotates the Vector2 from (0, 0)
        /// </summary>
        /// <param name="angle">Radians</param>
        public static Vector2 RotateBy(this Vector2 point, double angle)
        {
            float x = point.X * (float)Math.Cos(angle) - point.Y * (float)Math.Sin(angle);
            float y = point.X * (float)Math.Sin(angle) + point.Y * (float)Math.Cos(angle);
            return new Vector2(x, y);
        }
    }
}
