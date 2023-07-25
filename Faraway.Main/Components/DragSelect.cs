using System.Numerics;
using Faraway.Engine.Input;
using Faraway.Engine.Components;
using System.Diagnostics;
using Faraway.Main.GameObjects;

namespace Faraway.Main.Components
{
    public class DragSelect : Component
    {
        private Transform transform;
        private RectangleRenderer renderer;

        private Vector2 start = Vector2.Zero;
        private Vector2 end = Vector2.Zero;

        public SpaceCraft[] SelectedObjects;

        public override void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            renderer = GameObject.GetComponent<RectangleRenderer>();
            base.Start();
        }

        public override void Update(double deltaTime)
        {
            if (MouseInput.LeftButton.IsClickedDown)
                start = MouseInput.MousePosition;

            if (MouseInput.LeftButton.IsClickedUp)
                end = MouseInput.MousePosition;

            transform.Position = start;
            renderer.Size = end - start;

            Debug.WriteLine(start + " " + end);
            base.Update(deltaTime);
        }
    }
}
