using System.Numerics;
using tainicom.Aether.Physics2D.Dynamics;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;
using ABodyType = tainicom.Aether.Physics2D.Dynamics.BodyType;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Collision.Shapes;
using System;
using System.Diagnostics;
using tainicom.Aether.Physics2D.Common;
using Vector2 = System.Numerics.Vector2;
using Faraway.Engine.MathExtended;

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
    /// <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see> is the physics engine
    /// used. Most functionality should be wrapped so the video game's codebase that imports the game engine
    /// shouldn't have to import <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
    /// </summary>
    public sealed class RigidBody2D : Component
    {
        private Transform transform;

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
            PolygonShape box = new PolygonShape(1f);
            box.Vertices = PolygonTools.CreateRectangle(20f, 20f);
            Body.CreateFixture(box);
            Body.IgnoreGravity = true;
            Body.FixedRotation = false;

            base.Start();
        }
        public override void Update(double deltaTime)
        {
            Debug.WriteLine(Rotation + " " + Body.Rotation);
            if (GameObject.ContainsComponent<BoxCollider2D>() && BoxCollider2Ds.Count > 0)
                throw new MemberAccessException("GameObject cannot contain BoxCollider" +
                    " and RigidBody2D while `RigidBody2D.BoxCollider2Ds` contains items.");

            base.Update(deltaTime);
        }

        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        public void ApplyLinearImpulse(Vector2 force)
        {
            Body.ApplyLinearImpulse(new AVector2(force.X, force.Y));
        }
        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        public void ApplyAngularImpulse(float torque)
        {
            Body.ApplyAngularImpulse(torque);
        }

        /// <summary>
        /// Add a BoxCollider2D
        /// </summary>
        /// <param name="boxCollider"></param>
        public void AddBoxCollider2D(BoxCollider2D boxCollider)
        {
            BoxCollider2Ds.Add(boxCollider);
            Body.CreateRectangle(boxCollider.Size.X, boxCollider.Size.Y, 0f, new AVector2(boxCollider.Offset.X, boxCollider.Offset.Y));
        }
    }
}
