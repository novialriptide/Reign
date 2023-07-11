using System.Collections.Generic;
using Faraway.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Engine
{
    public class Scene
    {
        public GameInstance GameInstance;
        public List<GameObject> GameObjects { get; }
        public Camera2D ActiveCamera;
        public bool IsHidden;
        public bool IsPaused;
        private GameObjectGroup spriteGroup;
        private GameObjectGroup fontGroup;

        public Scene()
        {
            IsHidden = false;
            IsPaused = false;

            GameObjects = new List<GameObject>();
            spriteGroup = new GameObjectGroup();
            fontGroup = new GameObjectGroup();
        }
        /// <summary>
        /// Add a <c>GameObject</c> to the <c>Scene</c>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public void AddGameObject(GameObject gameObject)
        {
            gameObject.Scene = this;

            GameObjects.Add(gameObject);
            gameObject.OnAdd();
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
        public void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameInstance.SpriteBatch;

            spriteBatch.Begin();

            GameInstance.GraphicsDevice.Clear(Color.White);

            foreach (GameObject gameObject in spriteGroup.Match<SpriteRenderer>(GameObjects.ToArray()))
            {
                var sprite2D = gameObject.GetComponent<SpriteRenderer>();
                Vector2 renderPosition = gameObject.GetComponent<Transform>().GetWorldPosition();

                if (sprite2D.Texture == null)
                    continue;

                spriteBatch.Draw(sprite2D.Texture, renderPosition, Color.White);
            }

            foreach (GameObject gameObject in fontGroup.Match<FontRenderer>(GameObjects.ToArray()))
            {
                var fontRenderer = gameObject.GetComponent<FontRenderer>();
                Vector2 renderPosition = gameObject.GetComponent<Transform>().GetWorldPosition();

                if (fontRenderer.SpriteFont == null)
                    continue;

                spriteBatch.DrawString(fontRenderer.SpriteFont,
                    fontRenderer.Text, renderPosition, fontRenderer.Color);
            }

            spriteBatch.End();
        }
        /// <summary>
        /// Called when <c>Scene</c> is destroyed.
        /// </summary>
        public virtual void OnDestroy() { }
    }
}
