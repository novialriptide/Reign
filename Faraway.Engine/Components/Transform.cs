using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Faraway.Engine.Components
{
    public sealed class Transform : Component
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
        /// Rotation of the Component in rotations.
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

                if (Parent.Children.Contains(value))
                    throw new ArgumentException("Cannot make circular reference of parents");

                if (value.Parent == value)
                    throw new ArgumentException("Cannot make object a parent of itself.");

                parent = value;
            }
        }
        /// <summary>
        /// Check if this game object is a child.
        /// </summary>
        public bool IsChild => Parent is not null;
        /// <summary>
        /// Children of this GameObject.
        /// </summary>
        public Transform[] Children
        {
            get
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
        }
        /// <summary>
        /// The position of the GameObject based on the world.
        /// </summary>
        public Vector2 WorldPosition
        {
            get
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
        }
        public Vector2 ScreenPosition
        {
            get
            {
                Transform camera = GameObject.Scene.ActiveCamera.GameObject.GetComponent<Transform>();
                return camera.Position + WorldPosition;
            }
        }
        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPositionFromCamera()
        {
            return Vector2.Zero;
        }

        [Obsolete("Use property `WorldPosition` instead.")]
        public Vector2 GetWorldPosition() => WorldPosition;
    }
}
