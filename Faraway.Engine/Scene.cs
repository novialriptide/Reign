﻿using System.Collections.Generic;
using System.Diagnostics;
using Faraway.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Engine
{
    public abstract class Scene
    {
        public GameInstance GameInstance;
        public readonly List<GameObject> GameObjects;
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
                var transform = gameObject.GetComponent<Transform>();
                var sprite2D = gameObject.GetComponent<SpriteRenderer>();
                if (!sprite2D.IsEnabled)
                    continue;

                Vector2 renderPosition = transform.Position;
                float renderRotation = transform.Rotation;
                /*
                 * PRIORITY TODO: `renderRotationOrigin` is not correctly implemented.
                 * 
                 * Basic Algorithm Idea
                 * 1. Create variable `rotationPosition` as a `Vector2`.
                 * 2. Starting with any child, add the child's `Transform.Position + Transform.RotationOrigin` to `rotationPosition`.
                 * 3. Repeat until you've reached the parent root. Do not add the parent root's `Position`, but only add `Transform.RotationOrigin`.
                 */
                Vector2 renderRotationOrigin = transform.RotationOrigin;
                Vector2 renderScale = transform.Scale;

                Transform currentTransform = transform.Parent;

                while (currentTransform != null)
                {
                    renderPosition += currentTransform.Position;
                    renderRotation += currentTransform.Rotation;
                    renderRotationOrigin += currentTransform.Position;
                    renderScale *= currentTransform.Scale;

                    // Next loop
                    currentTransform = currentTransform.Parent;
                }


                if (sprite2D.Texture == null)
                    continue;

                spriteBatch.Draw(sprite2D.Texture, renderPosition, null, Color.White, renderRotation, renderRotationOrigin, renderScale, SpriteEffects.None, 0);
            }

            foreach (GameObject gameObject in fontGroup.Match<FontRenderer>(objs))
            {
                var fontRenderer = gameObject.GetComponent<FontRenderer>();
                if (!fontRenderer.IsEnabled)
                    continue;

                Vector2 renderPosition = gameObject.GetComponent<Transform>().GetWorldPosition();

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

                Vector2 renderPosition = gameObject.GetComponent<Transform>().GetWorldPosition() + rectRenderer.Offset;

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
