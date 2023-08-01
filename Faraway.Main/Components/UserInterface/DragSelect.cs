using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Faraway.Engine;
using Faraway.Engine.Components;
using Faraway.Engine.Input;
using Faraway.Main.GameObjects;
using Microsoft.Xna.Framework;
using Vector2 = System.Numerics.Vector2;

namespace Faraway.Main.Components.UserInterface
{
    public class DragSelect : Component
    {
        private Transform transform;
        private RectangleRenderer renderer;
        private BoxCollider2D boxCollider;

        public bool IsActivelyDragging;
        private Vector2 start = Vector2.Zero;
        private Vector2 end = Vector2.Zero;

        public List<SelectableObject> SelectedObjects = new List<SelectableObject>();

        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            renderer = GameObject.GetComponent<RectangleRenderer>();
            boxCollider = GameObject.GetComponent<BoxCollider2D>();

            renderer.Color = Color.Coral;

            base.Start();
        }

        public override void Update(double deltaTime)
        {
            if (MouseInput.LeftButton.IsClickedDown)
            {
                IsActivelyDragging = true;
                start = MouseInput.MousePosition;
            }

            if (MouseInput.LeftButton.IsHeldDown)
                end = MouseInput.MousePosition;

            var pos1 = new Vector2(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));
            var size = end - start;
            size.X = Math.Abs(size.X);
            size.Y = Math.Abs(size.Y);

            renderer.IsEnabled = IsActivelyDragging;
            transform.Position = pos1;
            renderer.Size = size;

            if (MouseInput.LeftButton.IsClickedUp)
            {
                boxCollider.Size = size;
                SelectedObjects.Clear();
                // Get the objects that collide with this object's BoxCollider2D.
                foreach (var obj in GameObject.Scene.GameObjects)
                {
                    bool isValidSelectable = obj.ContainsComponent<SelectableObject>() && obj.ContainsComponent<BoxCollider2D>();
                    if (isValidSelectable && obj.GetComponent<BoxCollider2D>().CollidesWith(boxCollider))
                    {
                        SelectedObjects.Add(obj.GetComponent<SelectableObject>());
                    }
                }
                IsActivelyDragging = false;
                Debug.WriteLine(SelectedObjects.Count);
            }

            base.Update(deltaTime);
        }
    }
}
