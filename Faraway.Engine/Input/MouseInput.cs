using System.Diagnostics;
using System.Numerics;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework.Input;

namespace Faraway.Engine.Input
{
    public static class MouseInput
    {
        private class MouseButton
        {
            /*
             * TODO: `IsPressed` is if the mouse button is being held down or not.
             * make an `IsClicked` as well.
             */
            private float timeSinceFirstPress = 0f;
            private bool isTimerActive = false;
            public bool IsPressed = false;
            public int ClickCount = 0;

            public void Update(float deltaTime, ButtonState buttonState)
            {
                IsPressed = buttonState == ButtonState.Pressed;

                if (IsPressed)
                    isTimerActive = true;

                if (isTimerActive)
                    timeSinceFirstPress += deltaTime;

                if (IsPressed && timeSinceFirstPress <= MouseDelay && isTimerActive)
                {
                    timeSinceFirstPress = 0;
                    ClickCount += 1;
                }
                else if (timeSinceFirstPress > MouseDelay && isTimerActive)
                {
                    timeSinceFirstPress = 0;
                    ClickCount = 0;
                    isTimerActive = false;
                }

                Debug.WriteLine(ClickCount);
            }
        }

        private static MouseState mouseState;

        /// <summary>
        /// Delay used to detect double click
        /// </summary>
        public static float MouseDelay = 0.5f;
        public static Vector2 MousePosition;

        private static MouseButton leftButton = new MouseButton();
        private static MouseButton middleButton = new MouseButton();
        private static MouseButton rightButton = new MouseButton();

        public static bool IsLeftDown => leftButton.IsPressed;
        public static bool IsRightDown => rightButton.IsPressed;
        public static bool IsMiddleDown => middleButton.IsPressed;

        /// <summary>
        /// Amount of times the left button has been clicked within
        /// `MouseInput.MouseDelay` seconds.
        /// </summary>
        public static int LeftClickCount => leftButton.ClickCount;
        /// <summary>
        /// Amount of times the middle button has been clicked within
        /// `MouseInput.MouseDelay` seconds.
        /// </summary>
        public static int MiddleClickCount => middleButton.ClickCount;
        /// <summary>
        /// Amount of times the right button has been clicked within
        /// `MouseInput.MouseDelay` seconds.
        /// </summary>
        public static int RightClickCount => rightButton.ClickCount;
        /// <summary>
        /// Stays 0 if no mouse scroll is detected.
        /// </summary>
        public static float MouseScrollDirection;

        public static void Update(float deltaTime)
        {
            mouseState = Mouse.GetState();

            MousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);

            leftButton.Update(deltaTime, mouseState.LeftButton);
            middleButton.Update(deltaTime, mouseState.MiddleButton);
            rightButton.Update(deltaTime, mouseState.RightButton);
        }
    }
}
