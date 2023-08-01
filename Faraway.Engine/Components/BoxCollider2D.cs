using System.Numerics;

namespace Faraway.Engine.Components
{
    /* 
     * PRIORITY TODO: Create a method in where multiple BoxCollider2Ds can be
     * collision checked for `DragSelect` in `Faraway.Main`.
     * 
     * Methods:
     *  - When collided, check the parent of the BoxCollider2D's game object to
     *    see if it has te `SelectableObject` component.
     *  - Make `SelectableObject` in the child game objects have a reference to
     *    the parent game object.
     */
    public sealed class BoxCollider2D : Component
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
