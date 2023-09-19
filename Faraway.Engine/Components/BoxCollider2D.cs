using Faraway.Engine.MathExtended;
using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;
using ATransform = tainicom.Aether.Physics2D.Common.Transform;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;
using Vector2 = System.Numerics.Vector2;

namespace Faraway.Engine.Components
{
    public sealed class BoxCollider2D : Component
    {
        private Transform transform;
        /// <summary>
        /// If a RigidBody2D is attached to this collider, the Size cannot be changed.
        /// </summary>
        public Vector2 Size;
        public float Density
        {
            get => Fixture.Shape.Density;
            set => Fixture.Shape.Density = value;
        }

        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        internal Fixture Fixture;
        internal ATransform AetherTransform => new ATransform(
                    new AVector2(transform.WorldPosition.X, transform.WorldPosition.Y), transform.Rotation);

        public BoxCollider2D() { }
        public BoxCollider2D(Vector2 size)
        {
            Size = size;
        }

        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();

            AVector2 center = new AVector2(Size.X, Size.Y) / 2;
            Vertices fixtureVertices = PolygonTools.CreateRectangle(Size.X / 2, Size.Y / 2, center, transform.Rotation);
            fixtureVertices.Translate(new AVector2(transform.Position.X, transform.Position.Y));

            PolygonShape rectangle;
            if (Fixture == null)
                rectangle = new PolygonShape(fixtureVertices, 1.0f);
            else
                rectangle = new PolygonShape(fixtureVertices, Density);

            Fixture = new Fixture(rectangle);

            base.Start();
        }

        public bool CollidesWith(BoxCollider2D collider)
        {
            Shape shape = Fixture.Shape;
            Shape otherShape = collider.Fixture.Shape;
            ATransform transform1 = AetherTransform;
            ATransform transform2 = collider.AetherTransform;
            return true;
            return Collision.TestOverlap(shape, 0, otherShape, 0, ref transform1, ref transform2);
        }
    }
}
