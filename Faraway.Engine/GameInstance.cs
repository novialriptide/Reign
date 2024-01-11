using System;
using System.Collections.Generic;
using System.Diagnostics;
using Faraway.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Engine
{
    public abstract class GameInstance : Game
    {
        public GraphicsDeviceManager GraphicsDeviceManager;
        public SpriteBatch SpriteBatch;

        public List<Scene> Scenes = new List<Scene>();

        public void AddScene(Scene scene)
        {
            scene.GameInstance = this;
            scene.OnStart();
            Scenes.Add(scene);
        }

        public double DeltaTime { get; private set; }
        public double FramesPerSecond { get; private set; }

        public void RemoveScene(Scene scene)
        {
            var foundScene = Scenes.Find(item => scene == item);

            if (foundScene is null)
                return;

            foundScene.OnDestroy();
            Scenes.Remove(foundScene);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            DeltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            FramesPerSecond = 1 / DeltaTime;

            MouseInput.Update((float)DeltaTime);
            foreach (Scene scene in Scenes)
                scene.Step(DeltaTime);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            DeltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            FramesPerSecond = 1 / DeltaTime;

            foreach (Scene scene in Scenes)
                scene.Draw();

            base.Draw(gameTime);
        }
    }
}
