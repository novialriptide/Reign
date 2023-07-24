using System.Numerics;
using Faraway.Engine.Input;
using Faraway.Engine.Components;
using System.Diagnostics;

namespace Faraway.Main.Components
{
    public class DragSelect : Component
    {
        private Vector2 start;
        private Vector2 end;

        public override void Update(double deltaTime)
        {
            Debug.WriteLine("lmao");

            /*
            Vector2 tempStart = Vector2.Zero;
            Vector2 tempEnd = Vector2.Zero;

            if (MouseInput.LeftButton.IsClickedDown)
                tempStart = MouseInput.MousePosition;
            else if (MouseInput.LeftButton.IsHeldDown)
                return;


            if (MouseInput.LeftButton.IsClickedUp)
                tempEnd = MouseInput.MousePosition;

            Debug.WriteLine(tempStart.ToString());
            Debug.WriteLine(tempEnd.ToString());
            */

            base.Update(deltaTime);
        }
    }
}
