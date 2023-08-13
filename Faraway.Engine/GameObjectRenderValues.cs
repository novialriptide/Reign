using System.Numerics;
using Faraway.Engine.Components;

namespace Faraway.Engine
{
    internal class GameObjectRenderValues
    {
        internal readonly Vector2 Position = Vector2.Zero;
        internal readonly float Rotation = 0f;
        internal readonly Vector2 RotationOrigin = Vector2.Zero;
        internal readonly Vector2 Scale = Vector2.One;

        internal GameObjectRenderValues(GameObject gameObject)
        {
            Transform currentTransform = gameObject.GetComponent<Transform>();
            while (currentTransform is not null)
            {
                Rotation += currentTransform.Rotation;

                if (currentTransform.IsChild)
                    RotationOrigin -= currentTransform.Position + currentTransform.RotationOrigin;

                Scale *= currentTransform.Scale;

                // Next loop
                currentTransform = currentTransform.Parent;

                /*
                 * Changing the rotation origin changes the render position, therefore we
                 * ignore the first position.
                 */
                if (currentTransform is not null)
                    Position += currentTransform.Position;
            }
        }
    }
}
