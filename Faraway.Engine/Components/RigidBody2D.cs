using System.Numerics;

namespace Faraway.Engine.Components
{
    /*
     * PRIORITY TODO: Utilize a physics library (probably Chipmunk2D) to implement proper
     * RigidBody2D functionality.
     */
    public sealed class RigidBody2D : Component
    {
        private Transform transform;
        public Vector2 Velocity = Vector2.Zero;
        public Vector2 Acceleration = Vector2.Zero;
        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            base.Start();
        }
        public override void Update(double deltaTime)
        {
            transform.Position += Velocity * (float)deltaTime;
            Velocity += Acceleration;
            base.Update(deltaTime);
        }
    }
}
