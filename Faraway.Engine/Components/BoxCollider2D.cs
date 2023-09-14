using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;
using ATransform = tainicom.Aether.Physics2D.Common.Transform;
using Vector2 = System.Numerics.Vector2;
using System.Diagnostics;
using Faraway.Engine.MathExtended;
using System;

namespace Faraway.Engine.Components
{
    public sealed class BoxCollider2D : Component
    {
        private Transform transform;
        private Vector2? queuedSizeBeforeStart = null;
        private Vector2 size;
        public Vector2 Size
        {
            get => size;
            set
            {
                if (transform is null)
                {
                    queuedSizeBeforeStart = value;
                    return;
                }

                size = value;
                Fixture = new Fixture(new PolygonShape(Vertices, Density));
            }
        }
        public float Density
        {
            get
            {
                float outValue = 1.0f;
                if (Fixture != null)
                    outValue = Fixture.Shape.Density;

                return outValue;
            }
            set => Fixture.Shape.Density = value;
        }

        internal Vertices Vertices
        {
            get
            {
                AVector2 center = new AVector2(Size.X, Size.Y) / 2;
                Vertices fixtureVertices = PolygonTools.CreateRectangle(Size.X / 2, Size.Y / 2, center, transform.Rotation);

                // Vector2 offset = transform.Position;
                // offset.RotateBy(transform.Rotation);
                // fixtureVertices.Translate(new AVector2(offset.X, offset.Y));
                // Debug.WriteLine(offset);

                return fixtureVertices;
            }
        }
        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        internal Fixture Fixture;
        internal ATransform AetherTransform => new ATransform(
                    new AVector2(transform.WorldPosition.X, transform.WorldPosition.Y), transform.Rotation);

        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            if (queuedSizeBeforeStart != null)
                Size = (Vector2)queuedSizeBeforeStart;

            base.Start();
        }

        public bool CollidesWith(BoxCollider2D collider)
        {
            Shape shape = Fixture.Shape;
            Shape otherShape = collider.Fixture.Shape;
            ATransform transform1 = AetherTransform;
            ATransform transform2 = collider.AetherTransform;
            return Collision.TestOverlap(shape, 0, otherShape, 0, ref transform1, ref transform2);
        }
    }
}
