using System.Numerics;
using Faraway.Engine.Input;
using Faraway.Engine.Components;
using System.Diagnostics;
using Faraway.Main.GameObjects;

namespace Faraway.Main.Components
{
    public class DragSelect : Component
    {
        private Vector2 start;
        private Vector2 end;

        public SpaceCraft[] SelectedObjects;

        public override void Update(double deltaTime)
        {
            if (MouseInput.LeftButton.IsClickedDown)
                start = MouseInput.MousePosition;

            if (MouseInput.LeftButton.IsClickedUp)
                end = MouseInput.MousePosition;

            Debug.WriteLine(start + " " + end);
            base.Update(deltaTime);
        }
    }
}
