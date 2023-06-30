using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Faraway.Main.Engine
{
    public class Scene
    {
        public List<GameObject> GameObjects { get; }
        public Scene()
        {
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
            GameObjects.Add(gameObject);

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
        public virtual void Update(GameTime gameTime) { }
        /// <summary>
        /// Called every frame; should contain draw logic.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Draw(GameTime gameTime) { }
        /// <summary>
        /// Called when <c>Scene</c> is destroyed.
        /// </summary>
        public virtual void OnDestroy() { }
    }
}
