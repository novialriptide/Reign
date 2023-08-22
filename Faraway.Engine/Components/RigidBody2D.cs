using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Faraway.Engine.MathExtended;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;
using ABodyType = tainicom.Aether.Physics2D.Dynamics.BodyType;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;
using Vector2 = System.Numerics.Vector2;

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

        private Dictionary<BoxCollider2D, Fixture> linkedColliders = new Dictionary<BoxCollider2D, Fixture>();

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

        public RigidBody2D() { }
        public RigidBody2D(bool ignoreGravity)
        {
            IgnoreGravity = ignoreGravity;
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
            registerColliders();
            Debug.WriteLine(Rotation);

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
            /*
             * PRIORITY TODO: `Children` must be `AllChildren`, `AllChildren` has performance issues.
             */
            // Get all child BoxCollider2D components.
            foreach (Transform child in transform.Children)
            {
                BoxCollider2D collider = child.GameObject.GetComponent<BoxCollider2D>();
                if (collider is not null && !linkedColliders.ContainsKey(collider))
                    addBoxCollider2D(collider);
            }
        }
        /// <summary>
        /// Add a BoxCollider2D.
        /// </summary>
        private void addBoxCollider2D(BoxCollider2D boxCollider)
        {
            /*
             * TOOD: BoxCollider2Ds are only added if they are the RigidBody2D's children, not
             * the RigidBody2D's children's children (and so on).
             * 
             * TODO: Rotation no longer works.
             */
            Transform childTransform = boxCollider.GameObject.GetComponent<Transform>();
            if (!childTransform.IsChildOf(transform))
                throw new Exception("Cannot add a BoxCollider2D to a RigidBody2D reference if" +
                    " the RigidBody2D's transform is not a parent of the BoxCollider2D.");

            AVector2 center = new AVector2(boxCollider.Size.X, boxCollider.Size.Y) / 2;
            Vertices fixtureVertices = PolygonTools.CreateRectangle(
                boxCollider.Size.X / 2, boxCollider.Size.Y / 2, center, childTransform.Rotation);

            fixtureVertices.Translate(new AVector2(childTransform.Position.X, childTransform.Position.Y));

            Shape rotatedRectangle = new PolygonShape(fixtureVertices, 0.0f);
            Fixture fixture = new Fixture(rotatedRectangle);

            Body.Add(fixture);
            linkedColliders.Add(boxCollider, fixture);
        }
    }
}
