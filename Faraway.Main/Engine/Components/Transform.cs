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
        /// <summary>
        /// Position of the Component.
        /// </summary>
        public Vector2 Position = Vector2.Zero;
        /// <summary>
        /// Scale of the Component; default set to <c>(1f, 1f)</c>.
        /// </summary>
        public Vector2 Scale = new Vector2(1f, 1f);
        /// <summary>
        /// Rotation of the Component in Radians.
        /// </summary>
        public float Rotation = 0f;
        /// <summary>
        /// Rotation Anchor of the Component.
        /// </summary>
        public Vector2 RotationOrigin = Vector2.Zero;
    }
}
