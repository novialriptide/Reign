using System.Collections.Generic;
using Reign.Engine.Components;

namespace Reign.Engine.MathExtended
{
    public class Algorithms
    {
        /// <summary>
        /// Returns missing components from the original.
        /// </summary>
        /// <param name="original">The <see cref="List{T}"/> that has all of the elements</param>
        /// <param name="modified">The <see cref="List{T}"/> that doesn't contain all of the elements</param>
        /// <returns></returns>
        public static List<BoxCollider2D> FindMissingComponents(List<BoxCollider2D> original, List<BoxCollider2D> modified)
        {
            List<BoxCollider2D> components = new List<BoxCollider2D>();
            foreach (BoxCollider2D originalComponent in original)
                if (!modified.Contains(originalComponent))
                    components.Add(originalComponent);

            return components;
        }
    }
}
