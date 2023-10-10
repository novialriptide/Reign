using System;
using System.Collections.Generic;
using System.Linq;
using Faraway.Engine.MathExtended;
using tainicom.Aether.Physics2D.Dynamics;
using ABodyType = tainicom.Aether.Physics2D.Dynamics.BodyType;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;
using Vector2 = System.Numerics.Vector2;

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
    /// to the <c>BoxCollider2Ds</c> array. It is not recommended to have multiple <see cref="BoxCollider2D"/>s as
    /// children if the game object with the <see cref="RigidBody2D"/> attached already has a <see cref="BoxCollider2D"/>.
    /// 
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
        public float AngularVelocity
        {
            get => MathFl.RadiansToDegrees(Body.AngularVelocity);
            set => Body.AngularVelocity = MathFl.DegreesToRadians(value);
        }
        /// <summary>
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        public Vector2 LocalCenter
        {
            get => new Vector2(Body.LocalCenter.X, Body.LocalCenter.Y);
            set => Body.LocalCenter = new AVector2(value.X, value.Y);
        }
        /// <summary>
        /// Gets the center of gravity of the rigid body.
        /// 
        /// Taken from <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
        /// </summary>
        public Vector2 WorldCenter => new Vector2(Body.WorldCenter.X, Body.WorldCenter.Y);
        public BodyType BodyType
        {
            get => (BodyType)Body.BodyType;
            set => Body.BodyType = (ABodyType)value;
        }
        public List<BoxCollider2D> BoxCollider2Ds { get; } = new List<BoxCollider2D>();

        public RigidBody2D() { }
        public RigidBody2D(bool ignoreGravity)
        {
            IgnoreGravity = ignoreGravity;
        }

        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();

            AVector2 av = new AVector2(transform.Position.X, transform.Position.Y);
            Body = Simulation.CreateBody(
                position: av,
                rotation: transform.Rotation,
                bodyType: (ABodyType)BodyType.Dynamic);
            Body.IgnoreGravity = true;
            Body.FixedRotation = false;

            syncFixtures();

            base.Start();
        }
        public override void Update(double deltaTime)
        {
            syncFixtures();

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

        /// <summary>
        /// Adds or removes Fixtures (from the physics engine) and BoxCollider2Ds that are
        /// supposed to be with the RigidBody2D
        /// </summary>
        private void syncFixtures()
        {
            List<BoxCollider2D> allBoxCollider2Ds = transform.GetComponentFromAllChildren<BoxCollider2D>().ToList();
            if (GameObject.ContainsComponent<BoxCollider2D>())
                allBoxCollider2Ds.Add(GameObject.GetComponent<BoxCollider2D>());

            if (allBoxCollider2Ds.ToList().Equals(BoxCollider2Ds))
                return; // Everything is up-to-date!

            List<BoxCollider2D> componentsToAdd = Algorithms.FindMissingComponents(allBoxCollider2Ds, BoxCollider2Ds);
            List<BoxCollider2D> componentsToRemove = Algorithms.FindMissingComponents(BoxCollider2Ds, allBoxCollider2Ds);

            foreach (BoxCollider2D collider in componentsToAdd)
                addBoxCollider2D(collider);

            foreach (BoxCollider2D collider in componentsToRemove)
                removeBoxCollider2D(collider);
        }
        /// <summary>
        /// Throws exception if the BoxCollider2D is not assignable to this RigidBody2D
        /// </summary>
        private void isAssignable(BoxCollider2D boxCollider)
        {
            Transform childTransform = boxCollider.GameObject.GetComponent<Transform>();
            if (!childTransform.IsChildOf(transform) && !GameObject.ContainsComponent<BoxCollider2D>())
                throw new Exception("This BoxCollider2D is not assignable to this RigidBody2D as it is not a child of this body and it's not one of the components in its game object.");
        }
        /// <summary>
        /// Add a BoxCollider2D.
        /// </summary>
        private void addBoxCollider2D(BoxCollider2D boxCollider)
        {
            isAssignable(boxCollider);
            if (Body.FixtureList.Contains(boxCollider.Fixture))
                throw new Exception("BoxCollider2D is already added to this RigidBody2D");

            Body.Add(boxCollider.Fixture);
            BoxCollider2Ds.Add(boxCollider);
        }
        /// <summary>
        /// Removes a BoxCollider2D.
        /// </summary>
        private void removeBoxCollider2D(BoxCollider2D boxCollider)
        {
            isAssignable(boxCollider);
            if (!Body.FixtureList.Contains(boxCollider.Fixture))
                throw new Exception("BoxCollider2D is not added to this RigidBody2D");

            Body.Remove(boxCollider.Fixture);
            BoxCollider2Ds.Remove(boxCollider);
        }
    }
}
