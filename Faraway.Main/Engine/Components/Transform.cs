using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Engine.Components
{
    public class Transform : Component
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 Scale = Vector2.Zero;
        public float Rotation = 0f;
        public Vector2 RotationOrigin = Vector2.Zero;
    }
}
