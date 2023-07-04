using Faraway.Main.Engine;
using Faraway.Main.Scenes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Faraway.Main
{
    public class FarawayGame : GameInstance
    {
        public FarawayGame()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Scenes = new List<Scene>();
        }

        protected override void LoadContent()
        {
            AddScene(new MainWorld());
            base.LoadContent();
        }
    }
}
