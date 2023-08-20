using tainicom.Aether.Physics2D.Dynamics;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;
using ABodyType = tainicom.Aether.Physics2D.Dynamics.BodyType;
using System.Collections.Generic;
using System;
using Vector2 = System.Numerics.Vector2;
using Faraway.Engine.MathExtended;
using System.Linq;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Collision.Shapes;

/*
 * PRIORITY TODO: Figure out the relationship between RigidBody2D and BoxCollider2D.
 * 
 * 1. Should the gameobject have both components? (RigidBody2D + BoxCollider2D + Transform)
 * 2. Should the gameobject have the RigidBody2D reference the BoxCollider2Ds for multiple collisions?
 *   2a. If so, what happens if one of the BoxCollider2Ds is detached from the parent?
 */

namespace Faraway.Engine.Components
{
    public enum BodyType
    {
        Static = ABodyType.Static,
        Dynamic = ABodyType.Dynamic,
        Kinematic = ABodyType.Kinematic
    }
    /// <summary>
    /// The BoxCollider2D component that is attached to this game object will automatically be added as a reference
    /// to the <c>BoxCollider2Ds</c> array. It is not recommended to have multiple `BoxCollider2D`s as children if
    /// the game object already has a `BoxCollider2D`.
    /// 
    /// <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see> is the physics engine
    /// used. Most functionality should be wrapped so the video game's codebase that imports the game engine
    /// shouldn't have to import <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
    /// </summary>
    public sealed class RigidBody2D : Component
    {
        private Transform transform;

        private Dictionary<int, Fixture> linkedColliders = new Dictionary<int, Fixture>();

        internal World Simulation => GameObject.Scene.Simulation;
        internal Body Body;

        /// <summary>
        /// Value to decide if the game object should ignore gravity or not.
        ///
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        public bool IgnoreGravity
        {
            get => Body.IgnoreGravity;
            set => Body.IgnoreGravity = value;
        }
        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        public Vector2 Velocity
        {
            get => new Vector2(Body.LinearVelocity.X, Body.LinearVelocity.Y);
            set => Body.LinearVelocity = new AVector2(value.X, value.Y);
        }
        public float Rotation
        {
            get => MathFl.RadiansToDegrees(Body.Rotation);
            set => Body.Rotation = MathFl.DegreesToRadians(value);
        }
        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        public Vector2 CenterOfGravity
        {
            get => new Vector2(Body.LocalCenter.X, Body.LocalCenter.Y);
            set => Body.LocalCenter = new AVector2(value.X, value.Y);
        }
        private HashSet<BoxCollider2D> boxCollider2Ds = new HashSet<BoxCollider2D>();
        /// <summary>
        /// If empty, then the BoxCollider2D assignd to this game object will be used.
        /// </summary>
        public readonly List<BoxCollider2D> BoxCollider2Ds = new List<BoxCollider2D>();

        public RigidBody2D() { }
        public RigidBody2D(bool ignoreGravity)
        {
            IgnoreGravity = ignoreGravity;
        }
        public RigidBody2D(bool ignoreGravity, List<BoxCollider2D> boxCollider2Ds)
        {
            IgnoreGravity = ignoreGravity;
            BoxCollider2Ds = boxCollider2Ds;
        }

        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();

            AVector2 av = new AVector2(transform.Position.X, transform.Position.Y);
            Body = Simulation.CreateBody(position: av, rotation: transform.Rotation, bodyType: ABodyType.Dynamic);
            Body.IgnoreGravity = true;
            Body.FixedRotation = false;

            base.Start();
        }
        public override void Update(double deltaTime)
        {
            // Handle if this game object contains a BoxCollider2D component.
            BoxCollider2D boxCollider2D = GameObject.GetComponent<BoxCollider2D>();
            if (boxCollider2D is not null)
                addBoxCollider2D(boxCollider2D);

            base.Update(deltaTime);
        }

        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        public void ApplyLinearImpulse(Vector2 force) => Body.ApplyLinearImpulse(new AVector2(force.X, force.Y));
        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        public void ApplyAngularImpulse(float torque) => Body.ApplyAngularImpulse(torque);

        private void registerColliders()
        {
            // Get all child BoxCollider2D components.
            foreach (Transform child in transform.AllChildren)
            {
                if (child.GameObject.ContainsComponent<BoxCollider2D>())
                {
                    BoxCollider2D collider = child.GameObject.GetComponent<BoxCollider2D>();
                    addBoxCollider2D(collider);
                }
            }
        }
        /// <summary>
        /// Add a BoxCollider2D.
        /// </summary>
        private void addBoxCollider2D(BoxCollider2D boxCollider)
        {
            Transform childTransform = boxCollider.GameObject.GetComponent<Transform>();
            if (!childTransform.IsChildOf(transform))
                throw new Exception("Cannot add a BoxCollider2D to a RigidBody2D reference if" +
                    " the RigidBody2D's transform is not a parent of the BoxCollider2D.");

            AVector2 center = new AVector2(boxCollider.Size.X, boxCollider.Size.Y) / 2;
            Vertices fixtureVertices = PolygonTools.CreateRectangle(
                boxCollider.Size.X / 2, boxCollider.Size.Y / 2, center, childTransform.Rotation);
            fixtureVertices.Translate(new AVector2(boxCollider.Offset.X, boxCollider.Offset.Y));

            Shape rotatedRectangle = new PolygonShape(fixtureVertices, 0.0f);
            Fixture fixture = new Fixture(rotatedRectangle);

            Body.Add(fixture);
            linkedColliders.Add(boxCollider.GetHashCode(), fixture);
        }
    }
}
