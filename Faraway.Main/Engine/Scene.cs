using Faraway.Main.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Faraway.Main.Engine
{
    public class Scene
    {
        public List<GameObject> GameObjects { get; }

        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        GameObjectGroup spriteGroup;

        public Scene(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;

            GameObjects = new List<GameObject>();
        }
        /// <summary>
        /// Add a <c>GameObject</c> to the <c>Scene</c>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public T AddGameObject<T>(GameObject gameObject) where T: GameObject
        {
            gameObject.Scene = this;

            GameObjects.Add(gameObject);

            return (T)gameObject;
        }
        /// <summary>
        /// Creates a GameObjectGroup assigned to the scene
        /// </summary>
        /// <returns>The created GameObjectGroup</returns>
        public GameObjectGroup CreateGameObjectGroup()
        {
            GameObjectGroup gameObjectGroup = new GameObjectGroup(this);


            return gameObjectGroup;
        }
        /// <summary>
        /// Called when <c>Scene</c> is loaded.
        /// </summary>
        public virtual void OnStart() { }
        /// <summary>
        /// Called every frame; Should contain game logic.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < GameObjects.Count; i++)
                GameObjects[i].Update(gameTime.ElapsedGameTime.TotalSeconds);
        }
        /// <summary>
        /// Called every frame; should contain draw logic.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            for (int i = 0; i < GameObjects.Count; i++) { }
            spriteBatch.End();
        }
        /// <summary>
        /// Called when <c>Scene</c> is destroyed.
        /// </summary>
        public virtual void OnDestroy() { }
    }
}
