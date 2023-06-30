using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Faraway.Main.Engine
{
    public class Scene
    {
        public List<GameObject> GameObjects { get; }
        public List<Drawable2DGameObject> Drawable2DGameObjects { get; }

        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        public Scene(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;

            GameObjects = new List<GameObject>();
            Drawable2DGameObjects = new List<Drawable2DGameObject>();
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

            if (gameObject is GameObject)
                GameObjects.Add(gameObject);
            else if (gameObject.GetType() == typeof(Drawable2DGameObject))
                Drawable2DGameObjects.Add((Drawable2DGameObject)gameObject);

            return (T)gameObject;
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
            for (int i = 0; i < Drawable2DGameObjects.Count; i++)
                Drawable2DGameObjects[i].Draw(spriteBatch);
            spriteBatch.End();
        }
        /// <summary>
        /// Called when <c>Scene</c> is destroyed.
        /// </summary>
        public virtual void OnDestroy() { }
    }
}
