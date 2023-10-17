using System;
using System.Collections.Generic;
using Faraway.Engine.MathExtended;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;
using Vector2 = System.Numerics.Vector2;

namespace Faraway.Engine.Components
{
    public sealed class BoxCollider2D : Component
    {
        /// <summary>
        /// If there is a <see cref="RigidBody2D"/> attached, then the body will go through the collider.
        /// </summary>
        public bool IsTrigger = false;
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

        private bool eventOnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            foreach (Component component in GameObject.Scene.Fixtures[fixtureA].GameObject.Components)
                component.OnCollisionEnter(new CollisionData(GameObject));

            foreach (Component component in GameObject.Scene.Fixtures[fixtureB].GameObject.Components)
                component.OnCollisionEnter(new CollisionData(GameObject));
            
            return true;
        }
        private void eventOnSeparation(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            foreach (Component component in GameObject.Scene.Fixtures[fixtureA].GameObject.Components)
                component.OnCollisionExit(new CollisionData(GameObject));

            foreach (Component component in GameObject.Scene.Fixtures[fixtureB].GameObject.Components)
                component.OnCollisionExit(new CollisionData(GameObject));
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
            Fixture.OnCollision += eventOnCollision;
            Fixture.OnSeparation += eventOnSeparation;

            /*
             * Add the Fixture to a dictionary in the scene so it can
             * easily be looked up.
             */
            GameObject.Scene.Fixtures[Fixture] = this;

            base.Start();
        }
        public override void OnDestroy()
        {
            GameObject.Scene.Fixtures.Remove(Fixture);
        }
        /// <summary>
        /// Deprecated: Utilize event handlers instead.
        /// 
        /// This never supported rotated BoxCollider2Ds.
        /// </summary>
        [Obsolete("Use the event handlers instead.")]
        public bool CollidesWith(BoxCollider2D collider)
        {
            if (!IsEnabled || !collider.IsEnabled)
                return false;

            Transform otherTransform = collider.GameObject.GetComponent<Transform>();

            return Collisions.RectToRect(transform.WorldPosition, Size,
                otherTransform.WorldPosition, collider.Size);
        }
    }
}
