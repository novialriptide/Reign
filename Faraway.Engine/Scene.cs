using System.Collections.Generic;
using Faraway.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Engine
{
    public abstract class Scene
    {
        public GameInstance GameInstance;
        public List<GameObject> GameObjects { get; }
        public Camera2D ActiveCamera;
        public bool IsHidden;
        public bool IsPaused;
        private GameObjectGroup spriteGroup;
        private GameObjectGroup fontGroup;
        private GameObjectGroup shapeGroup;

        public Scene()
        {
            IsHidden = false;
            IsPaused = false;

            GameObjects = new List<GameObject>();
            spriteGroup = new GameObjectGroup();
            fontGroup = new GameObjectGroup();
            shapeGroup = new GameObjectGroup();
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

            GameObject[] objs = GameObjects.ToArray();

            foreach (GameObject gameObject in spriteGroup.Match<SpriteRenderer>(objs))
            {
                var sprite2D = gameObject.GetComponent<SpriteRenderer>();
                Vector2 renderPosition = gameObject.GetComponent<Transform>().GetWorldPosition();

                if (sprite2D.Texture == null)
                    continue;

                spriteBatch.Draw(sprite2D.Texture, renderPosition, Color.White);
            }

            foreach (GameObject gameObject in fontGroup.Match<FontRenderer>(objs))
            {
                var fontRenderer = gameObject.GetComponent<FontRenderer>();
                Vector2 renderPosition = gameObject.GetComponent<Transform>().GetWorldPosition();

                if (fontRenderer.SpriteFont == null)
                    continue;

                spriteBatch.DrawString(fontRenderer.SpriteFont,
                    fontRenderer.Text, renderPosition, fontRenderer.Color);
            }

            foreach (GameObject gameObject in shapeGroup.Match<RectangleRenderer>(objs))
            {
                var rectRenderer = gameObject.GetComponent<RectangleRenderer>();
                Vector2 renderPosition = gameObject.GetComponent<Transform>().GetWorldPosition() + rectRenderer.Offset;

                // TODO: Find a cleaner way to approach this. Texture2D being assigned here is not a great idea.
                rectRenderer.Texture ??= new Texture2D(GameInstance.GraphicsDevice, 1, 1);

                spriteBatch.Draw(rectRenderer.Texture, renderPosition, rectRenderer.Color);
            }

            spriteBatch.End();

        }
        /// <summary>
        /// Called when <c>Scene</c> is destroyed.
        /// </summary>
        public virtual void OnDestroy() { }
    }
}
