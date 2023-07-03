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

        private Transform _parent;
        public Transform Parent
        {
            get { return _parent; }
            set
            {
                if (Parent.GetChildren().Contains(value))
                    throw new ArgumentException("Cannot make circular reference of parents");

                if (value.Parent == value)
                    throw new ArgumentException("Cannot make object a parent of itself.");

                _parent = value;
            }
        }
        public Transform[] GetChildren()
        {
            List<Transform> children = new List<Transform>();

            foreach (GameObject go in GameObject.Scene.GameObjects)
            {
                Transform t = go.GetComponent<Transform>();
                if (t.Parent == this)
                    children.Add(t);
            }

            return children.ToArray();
        }
        public Vector2 GetWorldPosition()
        {
            return Vector2.Zero;
        }
        public Vector2 GetScreenPosition()
        {
            return Vector2.Zero;
        }
        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPositionFromCamera()
        {
            return Vector2.Zero;
        }
    }
}
