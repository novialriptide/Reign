using System.Numerics;
using Microsoft.Xna.Framework.Input;

namespace Reign.Engine.Input
{
    public static class MouseInput
    {
        private static MouseState mouseState;

        /// <summary>
        /// Delay used to detect double click in milliseconds.
        /// </summary>
        public static int MouseDelay = 500;
        public static Vector2 MousePosition;

        public static MouseButton LeftButton = new MouseButton();
        public static MouseButton MiddleButton = new MouseButton();
        public static MouseButton RightButton = new MouseButton();

        /// <summary>
        /// Stays 0 if no mouse scroll is detected.
        /// </summary>
        public static float MouseScrollDirection;

        internal static void Update(float deltaTime)
        {
            mouseState = Mouse.GetState();

            MousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);

            LeftButton.Update(deltaTime, mouseState.LeftButton, MouseDelay);
            MiddleButton.Update(deltaTime, mouseState.MiddleButton, MouseDelay);
            RightButton.Update(deltaTime, mouseState.RightButton, MouseDelay);
        }
    }
}
