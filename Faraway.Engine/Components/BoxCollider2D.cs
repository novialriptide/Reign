using System;
using System.Collections.Generic;
using System.Linq;
using Faraway.Engine.MathExtended;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using ABodyType = tainicom.Aether.Physics2D.Dynamics.BodyType;
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
        private Vector2 size;
        public Vector2 Size
        {
            get
            {
                Vertices vertices = ((PolygonShape)Fixture.Shape).Vertices;
                if (vertices.Count != 4)
                    throw new Exception("BoxCollider2D has an invalid amount of vertices.");

                List<float> x = new() { vertices[0].X, vertices[1].X, vertices[2].X, vertices[3].X };
                List<float> y = new() { vertices[0].Y, vertices[1].Y, vertices[2].Y, vertices[3].Y };

                float topX = x.Min();
                float topY = y.Min();
                float bottomX = x.Max();
                float bottomY = y.Max();

                return new Vector2(bottomX - topX, bottomY - topY);
            }
            set
            {
                // TODO: Fixture.Shape is *readonly*, make this assignable somehow.
                Body.Remove(Fixture);
                Fixture f = createFixture(value);
                Fixture = f;
                Body.Add(Fixture);
            }
        }
        public float Density
        {
            get => Fixture.Shape.Density;
            set => Fixture.Shape.Density = value;
        }

        internal World Simulation => GameObject.Scene.Simulation;
        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        internal Fixture Fixture;
        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// 
        /// Remains dormant if a <see cref="RigidBody2D"/> is attached. 
        /// </summary>
        internal Body Body;

        /// <summary>
        /// Returns null if no <see cref="RigidBody2D"/> is assigned.
        /// </summary>
        public RigidBody2D AssignedRigidBody2D { get; internal set; }
        internal bool IsInternalBodyDormant => AssignedRigidBody2D is not null;

        private List<Component> componentsCollided = new();
        /// <summary>
        /// Used internally to call <see cref="Component.OnCollisionEnter(CollisionData)"/> 
        /// </summary>
        private bool eventOnCollision(Fixture fixtureA, Fixture _, Contact __)
        {
            // `fixtureB` would later become `fixtureA` in a different event call.

            foreach (Component component in GameObject.Scene.Fixtures[fixtureA].GameObject.Components)
            {
                component.OnCollisionEnter(new CollisionData(GameObject));
                componentsCollided.Add(component);
            }

            return true;
        }
        private void eventOnSeparation(Fixture fixtureA, Fixture _, Contact __)
        {
            // `fixtureB` would later become `fixtureA` in a different event call.

            foreach (Component component in GameObject.Scene.Fixtures[fixtureA].GameObject.Components)
            {
                component.OnCollisionExit(new CollisionData(GameObject));
                componentsCollided.Remove(component);
            }
        }

        private Fixture createFixture(Vector2 size)
        {
            // Create actual collider using Aether.Physics2D
            AVector2 center = new AVector2(size.X, size.Y) / 2;
            Vertices fixtureVertices = PolygonTools.CreateRectangle(size.X / 2, size.Y / 2, center, transform.Rotation);
            fixtureVertices.Translate(new AVector2(transform.Position.X, transform.Position.Y));

            PolygonShape rectangle;
            if (Fixture is null)
                rectangle = new PolygonShape(fixtureVertices, 1.0f);
            else
                rectangle = new PolygonShape(fixtureVertices, Density);

            Fixture f = new Fixture(rectangle);
            f.OnCollision += eventOnCollision;
            f.OnSeparation += eventOnSeparation;

            return f;
        }

        public BoxCollider2D(Vector2 size)
        {
            this.size = size;
        }

        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();

            Fixture = createFixture(size);

            AVector2 av = new AVector2(transform.Position.X, transform.Position.Y);
            Body = Simulation.CreateBody(
                position: av,
                rotation: transform.Rotation,
                bodyType: ABodyType.Dynamic
            );
            Body.IgnoreGravity = true;
            Body.FixedRotation = true;

            if (!GameObject.ContainsComponent<RigidBody2D>())
                Body.Add(Fixture);

            // Add the Fixture to a dictionary in the scene so it can easily be looked up.
            GameObject.Scene.Fixtures[Fixture] = this;

            base.Start();
        }
        public override void Update(double deltaTime)
        {
            foreach (Component component in componentsCollided)
            {
                component.OnCollisionWhile(new CollisionData(GameObject));
            }
            base.Update(deltaTime);
        }
        public override void OnDestroy()
        {
            if (!GameObject.ContainsComponent<RigidBody2D>())
                Body.Remove(Fixture);

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

            return Collisions.RectToRect(transform.WorldPosition, size,
                otherTransform.WorldPosition, collider.size);
        }
    }
}
