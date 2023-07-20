using System.Numerics;
using Microsoft.Xna.Framework.Input;

namespace Faraway.Engine
{
    public class MouseInput
    {
        private MouseState mouseState;

        /// <summary>
        /// Delay used to detect double click
        /// </summary>
        public float MouseDelay;

        public Vector2 MousePosition;
        public bool IsLeftDown;
        public bool IsRightDown;
        public bool IsMiddleDown;
        /// <summary>
        /// Amount of times the left button has been clicked within
        /// `MouseInput.MouseDelay` seconds.
        /// </summary>
        public int LeftClickCount;
        /// <summary>
        /// Amount of times the middle button has been clicked within
        /// `MouseInput.MouseDelay` seconds.
        /// </summary>
        public int MiddleClickCount;
        /// <summary>
        /// Amount of times the right button has been clicked within
        /// `MouseInput.MouseDelay` seconds.
        /// </summary>
        public int RightClickCount;
        /// <summary>
        /// Stays 0 if no mouse scroll is detected.
        /// </summary>
        public float MouseScrollDirection;

        public void Update()
        {
            mouseState = Mouse.GetState();

            MousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            IsLeftDown = mouseState.LeftButton == ButtonState.Pressed;
            IsMiddleDown = mouseState.MiddleButton == ButtonState.Pressed;
            IsRightDown = mouseState.RightButton == ButtonState.Pressed;
        }
    }
}
