using Faraway.Main.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Faraway.Main.Engine
{
    public class Scene
    {
        public GameInstance GameInstance;
        public bool IsHidden;
        public bool IsPaused;
        public List<GameObject> GameObjects { get; }

        GameObjectGroup spriteGroup;

        public Scene()
        {
            IsHidden = false;
            IsPaused = false;

            GameObjects = new List<GameObject>();
            spriteGroup = new GameObjectGroup();
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
            SpriteBatch spriteBatch = GameInstance.SpriteBatch;

            spriteBatch.Begin();
            foreach(GameObject gameObject in spriteGroup.Match<Sprite2D>(GameObjects.ToArray()))
            {
                var transform = gameObject.GetComponent<Transform>();
                var sprite2D = gameObject.GetComponent<Sprite2D>();

                GameObject obj = gameObject;
                Vector2 renderPosition = Vector2.Zero;

                while (obj != null)
                {
                    renderPosition += obj.GetComponent<Transform>().Position;
                    obj = obj.Parent;
                }

                spriteBatch.Draw(sprite2D.texture, renderPosition, Color.White);
            }
            spriteBatch.End();
        }
        /// <summary>
        /// Called when <c>Scene</c> is destroyed.
        /// </summary>
        public virtual void OnDestroy() { }
    }
}
