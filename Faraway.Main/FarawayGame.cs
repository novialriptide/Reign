﻿using System.Collections.Generic;
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
            IsFixedTimeStep = false;
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = false; // vsync

            Scenes = new List<Scene>();
        }

        protected override void LoadContent()
        {
            AddScene(new MainWorld());
            base.LoadContent();
        }
    }
}
