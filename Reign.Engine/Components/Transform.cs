using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;

namespace Reign.Engine.Components
{
    /// <summary>
    /// <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see> is the physics engine
    /// used. Most functionality should be wrapped so the video game's codebase that imports the game engine
    /// shouldn't have to import <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>.
    ///
    /// If a <c>RigidBody2D</c> component isn't attached to the game object, then the component will not rely
    /// on the physics engine.
    /// </summary>
    public sealed class Transform : Component
    {
        private enum TransformDependent
        {
            Self,
            BoxCollider2D,
            RigidBody2D
        }

        /// <summary>
        /// This component is never meant to be disabled.
        /// </summary>
        public new bool IsEnabled => true;
        private TransformDependent selectedTransform
        {
            get
            {
                RigidBody2D rigidBody2D = GameObject.GetComponent<RigidBody2D>();
                BoxCollider2D boxCollider2D = GameObject.GetComponent<BoxCollider2D>();

                if (rigidBody2D is not null && rigidBody2D.IsEnabled)
                    return TransformDependent.RigidBody2D;

                else if (boxCollider2D is not null && boxCollider2D.IsEnabled)
                    return TransformDependent.BoxCollider2D;

                return TransformDependent.Self;
            }
        }
        private Vector2 position = Vector2.Zero;
        /// <summary>
        /// Position of the Component.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                switch (selectedTransform)
                {
                    case TransformDependent.Self:
                        return position;
                    case TransformDependent.BoxCollider2D:
                        BoxCollider2D bc = GameObject.GetComponent<BoxCollider2D>();
                        if (bc.Body is null)
                            return position;

                        return new Vector2(bc.Body.Position.X, bc.Body.Position.Y);
                    case TransformDependent.RigidBody2D:
                        RigidBody2D rb = GameObject.GetComponent<RigidBody2D>();
                        if (rb.Body is null)
                            return position;

                        return new Vector2(rb.Body.Position.X, rb.Body.Position.Y);
                }

                return position;
            }
            set
            {
                switch (selectedTransform)
                {
                    case TransformDependent.Self:
                        position = value;
                        return;
                    case TransformDependent.BoxCollider2D:
                        BoxCollider2D bc = GameObject.GetComponent<BoxCollider2D>();
                        if (bc.Body is null)
                        {
                            position = value;
                            return;
                        }
                        bc.Body.SetTransform(new AVector2(value.X, value.Y), Rotation);
                        return;
                    case TransformDependent.RigidBody2D:
                        RigidBody2D rb = GameObject.GetComponent<RigidBody2D>();
                        rb.Body.SetTransform(new AVector2(value.X, value.Y), Rotation);
                        return;
                }

                position = value;
            }
        }
        public float X
        {
            get => Position.X;
            set => Position = new Vector2(value, Position.Y);
        }
        public float Y
        {
            get => Position.Y;
            set => Position = new Vector2(Position.X, value);
        }
        /// <summary>
        /// Scale of the Component; default set to <c>(1f, 1f)</c>.
        /// </summary>
        public Vector2 Scale = new Vector2(1f, 1f);
        private float rotation = 0f;
        /// <summary>
        /// Rotation of the Component in radians.
        /// </summary>
        public float Rotation
        {
            get
            {
                switch (selectedTransform)
                {
                    case TransformDependent.Self:
                        return rotation;
                    case TransformDependent.BoxCollider2D:
                        BoxCollider2D bc = GameObject.GetComponent<BoxCollider2D>();
                        if (bc.Body is null)
                            return rotation;

                        return bc.Body.Rotation;
                    case TransformDependent.RigidBody2D:
                        RigidBody2D rb = GameObject.GetComponent<RigidBody2D>();
                        if (rb.Body is null)
                            return rotation;

                        return rb.Body.Rotation;
                }

                return rotation;
            }
            set
            {
                switch (selectedTransform)
                {
                    case TransformDependent.Self:
                        rotation = value;
                        return;
                    case TransformDependent.BoxCollider2D:
                        BoxCollider2D bc = GameObject.GetComponent<BoxCollider2D>();
                        bc.Body.SetTransform(bc.Body.Position, value);
                        return;
                    case TransformDependent.RigidBody2D:
                        RigidBody2D rb = GameObject.GetComponent<RigidBody2D>();
                        rb.Body.SetTransform(rb.Body.Position, value);
                        return;
                }

                rotation = value;
            }
        }
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
                if (Parent is null)
                {
                    parent = value;
                    return;
                }

                if (Parent.Children.Contains(value))
                    throw new ArgumentException("Cannot make circular reference of parents");

                if (value is not null && value.Parent == value)
                    throw new ArgumentException("Cannot make object a parent of itself.");

                parent = value;
            }
        }
        /// <summary>
        /// Add a child to this game object
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(Transform child) => child.parent = this;
        /// <summary>
        /// Check if this game object is a child.
        /// </summary>
        public bool IsChild => Parent is not null;
        /// <summary>
        /// Check if this game object is a child of the specified game object.
        /// </summary>
        public bool IsChildOf(Transform parent) => parent.Children.Contains(this);
        /// <summary>
        /// Check if this game object is a parent of the specified game object.
        /// </summary>
        public bool IsParentOf(Transform child) => Children.Contains(child);
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
        private List<Transform> allChildren(List<Transform> children)
        {
            foreach (Transform t in Children)
            {
                children.Add(t);
                t.allChildren(children);
            }
            return children;
        }
        /// <summary>
        /// Recurisvely retrieves all of the children game objects.
        /// </summary>
        public Transform[] AllChildren => allChildren(new List<Transform>()).ToArray();

        private T[] getComponentFromArray<T>(Transform[] transforms) where T : Component
        {
            List<T> components = new List<T>();
            foreach (Transform transform in transforms)
            {
                T component = transform.GameObject.GetComponent<T>();
                if (component is not null)
                    components.Add(component);
            }

            return components.ToArray();
        }
        public T[] GetComponentFromChildren<T>() where T : Component => getComponentFromArray<T>(Children);
        public T[] GetComponentFromAllChildren<T>() where T : Component => getComponentFromArray<T>(AllChildren);

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
    }
}
