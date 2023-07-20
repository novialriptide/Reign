using System.Numerics;

namespace Faraway.Engine.Components
{
    public class BoxCollider2D : Component
    {
        public Vector2 Offset;
        public Vector2 Size;
        public bool CollidesWith(BoxCollider2D collider)
        {
            return Offset.X + Size.X >= collider.Offset.X &&
                Offset.X <= collider.Offset.X + collider.Size.X &&
                Offset.Y + Size.Y >= collider.Offset.Y &&
                Offset.Y <= collider.Offset.Y + collider.Size.Y;
        }
    }
}
