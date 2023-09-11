using System;
using System.Diagnostics;
using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;
using ATransform = tainicom.Aether.Physics2D.Common.Transform;
using Vector2 = System.Numerics.Vector2;

namespace Faraway.Engine.Components
{
    internal class ReignFixtureData
    {
        public BoxCollider2D BoxCollider2D;
    }
    public sealed class BoxCollider2D : Component
    {
        private Transform transform;
        public Vector2 Size;

        private Fixture fixture;
        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        internal Fixture Fixture
        {
            get => fixture;
            set
            {
                fixture = value;
                fixture.Tag = new ReignFixtureData
                {
                    BoxCollider2D = this,
                };
                fixture.Body.OnCollision += onCollision;
                fixture.Body.OnSeparation += onSeparation;
            }
        }

        /// <summary>
        /// Relies on <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        internal Vertices Vertices
        {
            get
            {
                AVector2 center = new AVector2(Size.X, Size.Y) / 2;
                Vertices vertices = PolygonTools.CreateRectangle(Size.X / 2, Size.Y / 2, center, transform.Rotation);
                vertices.Translate(new AVector2(transform.Position.X, transform.Position.Y));
                return vertices;
            }
        }

        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            base.Start();
        }

        private bool onCollision(Fixture sender, Fixture other, Contact contact)
        {
            if (other.Tag == null)
                throw new Exception("Fixture does not contain game engine data.");

            BoxCollider2D otherCollider = ((ReignFixtureData)other.Tag).BoxCollider2D;
            foreach (Component c in GameObject.Components)
                c.OnCollisionEnter(otherCollider);

            return true;
        }

        private void onSeparation(Fixture sender, Fixture other, Contact contact)
        {
            if (other.Tag == null)
                throw new Exception("Fixture does not contain game engine data.");

            BoxCollider2D otherCollider = ((ReignFixtureData)other.Tag).BoxCollider2D;
            foreach (Component c in GameObject.Components)
                c.OnCollisionExit(otherCollider);
        }

        [Obsolete("Use `GameObject.OnCollisionEnter` & `GameObject.OnCollisionExit`")]
        public bool CollidesWith(BoxCollider2D collider)
        {
            if (collider.Fixture is null)
                throw new InvalidOperationException("Fixture is not initialized. Is the collider assigned to a GameObject?");

            PolygonShape rectangle = new PolygonShape(Vertices, 1.0f);
            ATransform aTransform1 = new ATransform(new AVector2(transform.Position.X, transform.Position.Y), transform.Rotation);

            Transform otherTransform = collider.GameObject.GetComponent<Transform>();
            ATransform aTransform2 = new ATransform(new AVector2(otherTransform.Position.X, otherTransform.Position.Y), otherTransform.Rotation);

            if (collider.Fixture is null)
            {
                Debug.WriteLine(collider.fixture);
                return false;
            }

            return Collision.TestOverlap(rectangle, 0, collider.Fixture.Shape, 0, ref aTransform1, ref aTransform2);
        }
    }
}
