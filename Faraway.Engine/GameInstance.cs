using System;
using System.Collections.Generic;
using Faraway.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Engine
{
    public abstract class GameInstance : Game
    {
        public GraphicsDeviceManager GraphicsDeviceManager;
        public SpriteBatch SpriteBatch;

        public List<Scene> Scenes;

        public void AddScene(Scene scene)
        {
            scene.GameInstance = this;
            scene.OnStart();
            Scenes.Add(scene);
        }

        public double DeltaTime;

        public void RemoveScene(Scene scene)
        {
            var foundScene = Scenes.Find(item => scene == item);

            if (foundScene == null)
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
            MouseInput.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            foreach (Scene scene in Scenes)
                if (!scene.IsPaused)
                    scene.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            DeltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Scene scene in Scenes)
                if (!scene.IsHidden)
                    scene.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
