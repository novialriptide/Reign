using System;
using System.Diagnostics;
using System.Numerics;
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

        public bool IsActivelyDragging;
        private Vector2 start = Vector2.Zero;
        private Vector2 end = Vector2.Zero;

        public SelectableObject[] SelectedObjects;

        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            renderer = GameObject.GetComponent<RectangleRenderer>();

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

            if (MouseInput.LeftButton.IsClickedUp)
                IsActivelyDragging = false;

            var pos1 = new Vector2(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));
            var size = end - start;
            size.X = Math.Abs(size.X);
            size.Y = Math.Abs(size.Y);

            renderer.IsEnabled = IsActivelyDragging;
            transform.Position = pos1;
            renderer.Size = size;

            base.Update(deltaTime);
        }
    }
}
