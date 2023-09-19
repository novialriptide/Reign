using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using AVector2 = tainicom.Aether.Physics2D.Common.Vector2;

namespace Faraway.Engine.Components
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
        public new bool IsEnabled => true; /* Never meant to be disabled */
        private bool checkRigidBody2D
        {
            get
            {
                RigidBody2D rigidBody2D = GameObject.GetComponent<RigidBody2D>();
                return rigidBody2D is not null && rigidBody2D.IsEnabled;
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
                if (checkRigidBody2D)
                {
                    RigidBody2D rb = GameObject.GetComponent<RigidBody2D>();
                    if (rb.Body is null)
                        return position;

                    return new Vector2(rb.Body.Position.X, rb.Body.Position.Y);
                }
                return position;
            }
            set
            {
                if (checkRigidBody2D)
                {
                    RigidBody2D rb = GameObject.GetComponent<RigidBody2D>();
                    rb.Body.SetTransform(new AVector2(value.X, value.Y), Rotation);
                    return;
                }
                position = value;
            }
        }
        private Vector2 scale = new Vector2(1f, 1f);
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
                if (checkRigidBody2D)
                {
                    RigidBody2D rb = GameObject.GetComponent<RigidBody2D>();
                    if (rb.Body is null)
                        return rotation;

                    return rb.Body.Rotation;
                }
                return rotation;
            }
            set
            {
                if (checkRigidBody2D)
                {
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
        private Transform[] allChildren(List<Transform> children)
        {
            foreach (Transform child in Children)
            {
                children.Add(child);
                children = (List<Transform>)children.Concat(allChildren(children)); // NOTE: Concat's memory allocation is terrible.
            }
            return children.ToArray();
        }
        /// <summary>
        /// Gets all children of this game object.
        /// </summary>
        public Transform[] AllChildren => allChildren(new List<Transform>());

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
    }
}
