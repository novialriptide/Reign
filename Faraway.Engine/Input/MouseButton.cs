using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace Faraway.Engine.Input
{
    public class MouseButton
    {
        private ButtonState lastState;
        private Timer clickerTimer = new Timer();
        /// <summary>
        /// Returns `true` if mouse button is being held down.
        /// </summary>
        public bool IsHeldDown = false;

        public bool IsClickedDown = false;
        public bool IsClickedUp = false;
        /// <summary>
        /// Amount of clicks within `mouseDelay` per click.
        /// </summary>
        public int ClickCount = 0;
        internal void Update(float deltaTime, ButtonState buttonState, double mouseDelay)
        {
            IsHeldDown = buttonState == ButtonState.Pressed;

            IsClickedDown = IsHeldDown && lastState == ButtonState.Released;
            IsClickedUp = !IsHeldDown && lastState == ButtonState.Pressed;

            lastState = buttonState;
        }
    }
}
