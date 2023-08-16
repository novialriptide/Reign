using System.Numerics;
using tainicom.Aether.Physics2D.Dynamics;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;
using ABodyType = tainicom.Aether.Physics2D.Dynamics.BodyType;

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

        public bool IgnoreGravity
        {
            get => Body.IgnoreGravity;
            set => Body.IgnoreGravity = value;
        }
        public Vector2 Velocity
        {
            get => new Vector2(Body.LinearVelocity.X, Body.LinearVelocity.Y);
            set => Body.LinearVelocity = new AVector2(value.X, value.Y);
        }
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

            base.Start();
        }
        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
        }

        public void ApplyLinearImpulse(Vector2 force)
        {
            Body.ApplyLinearImpulse(new AVector2(force.X, force.Y));
        }
    }
}
