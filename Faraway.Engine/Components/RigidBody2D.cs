using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Engine.Components
{
    public class RigidBody2D : Component
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
