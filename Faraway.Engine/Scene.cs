using System.Collections.Generic;
using Faraway.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Faraway.Engine
{
    public abstract class Scene
    {
        public GameInstance GameInstance;
        public readonly List<GameObject> GameObjects = new List<GameObject>();
        public Camera2D ActiveCamera;
        public bool IsHidden = false;
        public bool IsPaused = false;
        private GameObjectGroup spriteGroup = new GameObjectGroup();
        private GameObjectGroup fontGroup = new GameObjectGroup();
        private GameObjectGroup shapeGroup = new GameObjectGroup();

        internal readonly World Simulation = new World();

        public Scene() { }
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

            foreach (var component in gameObject.Components)
                component.Start();
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
        internal void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = GameInstance.SpriteBatch;

            spriteBatch.Begin();

            GameInstance.GraphicsDevice.Clear(Color.White);

            GameObject[] objs = GameObjects.ToArray();

            foreach (GameObject gameObject in spriteGroup.Match<SpriteRenderer>(objs))
            {
                var sprite2D = gameObject.GetComponent<SpriteRenderer>();

                if (sprite2D.Texture == null)
                    continue;

                if (!sprite2D.IsEnabled)
                    continue;

                GameObjectRenderValues values = new GameObjectRenderValues(gameObject);
                spriteBatch.Draw(sprite2D.Texture, values.Position, null, Color.White, values.Rotation, values.RotationOrigin, values.Scale, SpriteEffects.None, 0);
            }

            foreach (GameObject gameObject in fontGroup.Match<FontRenderer>(objs))
            {
                var fontRenderer = gameObject.GetComponent<FontRenderer>();
                if (!fontRenderer.IsEnabled)
                    continue;

                Vector2 renderPosition = gameObject.GetComponent<Transform>().WorldPosition;

                if (fontRenderer.SpriteFont == null)
                    continue;

                spriteBatch.DrawString(fontRenderer.SpriteFont,
                    fontRenderer.Text, renderPosition, fontRenderer.Color);
            }

            foreach (GameObject gameObject in shapeGroup.Match<RectangleRenderer>(objs))
            {
                var rectRenderer = gameObject.GetComponent<RectangleRenderer>();
                if (!rectRenderer.IsEnabled)
                    continue;

                Vector2 renderPosition = gameObject.GetComponent<Transform>().WorldPosition + rectRenderer.Offset;

                // TODO: Find a cleaner way to approach this. Texture2D being assigned here is not a great idea.
                if (rectRenderer.Texture == null)
                {
                    rectRenderer.Texture = new Texture2D(GameInstance.GraphicsDevice, 1, 1);
                    rectRenderer.Texture.SetData(new[] { Color.White });
                }

                Rectangle rect = new Rectangle
                {
                    X = (int)renderPosition.X,
                    Y = (int)renderPosition.Y,
                    Width = (int)rectRenderer.Size.X,
                    Height = (int)rectRenderer.Size.Y
                };

                spriteBatch.Draw(rectRenderer.Texture, rect, rectRenderer.Color);
            }

            spriteBatch.End();

        }
        /// <summary>
        /// Called when <c>Scene</c> is destroyed.
        /// </summary>
        public virtual void OnDestroy() { }
    }
}
