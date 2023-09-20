using Faraway.Engine.MathExtended;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;
using Vector2 = System.Numerics.Vector2;

namespace Faraway.Engine.Components
{
    public sealed class BoxCollider2D : Component
    {
        private Transform transform;
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
            if (!IsEnabled || !collider.IsEnabled)
                return false;

            /*
             * PRIORITY TODO: Rotation via Transform is not supported.
             * https://gist.github.com/jackmott/021bb1bd1135df71c389b42b8b44cc30
             */
            Transform otherTransform = collider.GameObject.GetComponent<Transform>();

            return Collisions.RectToRect(transform.WorldPosition, Size,
                otherTransform.WorldPosition, collider.Size);
        }
    }
}
