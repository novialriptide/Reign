using System.Collections.Generic;
using Faraway.Engine;
using Faraway.Main.Scenes;
using Microsoft.Xna.Framework;

namespace Faraway.Main
{
    public class FarawayGame : GameInstance
    {
        public FarawayGame()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false; // Priority TODO: DeltaTime is NOT correct.
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = false; // vsync
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 2000d); // cap framerate

            Scenes = new List<Scene>();
        }

        protected override void LoadContent()
        {
            AddScene(new MainWorld());
            base.LoadContent();
        }
    }
}
