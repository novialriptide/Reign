using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Transform parent;
        /// <summary>
        /// Parent of this GameObject.
        /// </summary>
        public Transform Parent
        {
            get => parent;
            set
            {
                if (Parent == null)
                {
                    parent = value;
                    return;
                }

                if (Parent.GetChildren().Contains(value))
                    throw new ArgumentException("Cannot make circular reference of parents");

                if (value.Parent == value)
                    throw new ArgumentException("Cannot make object a parent of itself.");

                parent = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Children of this <c>GameObject</c>.</returns>
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
            Vector2 value = Position;
            Transform currentTransform = Parent;

            while (currentTransform != null)
            {
                value += currentTransform.Position;
                currentTransform = currentTransform.Parent;
            }

            return value;
        }
        public Vector2 GetScreenPosition()
        {
            Transform camera = GameObject.Scene.ActiveCamera.GameObject.GetComponent<Transform>();
            return camera.Position + GetWorldPosition();
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
