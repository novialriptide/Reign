using System.Diagnostics;
using System.Numerics;

namespace Faraway.Engine.Components
{
    public sealed class BoxCollider2D : Component
    {
        private Transform transform;

        public Vector2 Size;

        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            base.Start();
        }

        public bool CollidesWith(BoxCollider2D collider)
        {
            Vector2 worldPosition = transform.WorldPosition;

            Transform otherTransform = collider.GameObject.GetComponent<Transform>();
            Vector2 otherWorldPosition = otherTransform.WorldPosition;

            return worldPosition.X + Size.X >= otherWorldPosition.X &&
                worldPosition.X <= otherWorldPosition.X + collider.Size.X &&
                worldPosition.Y + Size.Y >= otherWorldPosition.Y &&
                worldPosition.Y <= otherWorldPosition.Y + collider.Size.Y;
        }
    }
}
