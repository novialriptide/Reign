using System;
using System.Numerics;

namespace Faraway.Engine
{
    public static class MathExtended
    {
        /// <summary>
        /// Gets the magnitude.
        /// </summary>
        public static float Magnitude(Vector2 vector2) => MathF.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y);
        /// <summary>
        /// Sets the magnitude of the specified vector2.
        /// </summary>
        public static Vector2 SetMagnitude(Vector2 vector2, float magnitude)
        {
            // Referenced from: https://stackoverflow.com/questions/41317291/setting-the-magnitude-of-a-2d-vector
            float ratio = magnitude / Magnitude(vector2);
            return vector2 * ratio;
        }
        /// <summary>
        /// Moves a Vector2 toward the target.
        /// </summary>
        /// <returns>The updated position.</returns>
        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            // Referenced from: https://github.com/pygame/pygame/pull/2929
            Vector2 a = target - current;
            float magnitude = Magnitude(a);
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
                return target;

            return current + a / magnitude * maxDistanceDelta;
        }
        /// <summary>
        /// Moves a float toward the target.
        /// </summary>
        /// <returns>The updated value.</returns>
        public static float MoveTowards(float current, float target, float maxDistanceDelta)
        {
            if (MathF.Abs(target - current) <= maxDistanceDelta)
                return target;

            return current + MathF.Sign(target - current) * maxDistanceDelta;
        }
    }
}
