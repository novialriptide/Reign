using Faraway.Main.Engine;
using Faraway.Main.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Faraway.Main
{
    public class FarawayGame : Game
    {
        public static GraphicsDeviceManager GraphicsDeviceManager;
        private SpriteBatch _spriteBatch;

        private Sprite2D _sprite;

        private SpaceCraft playerSpaceCraft;

        public FarawayGame()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            playerSpaceCraft = new SpaceCraft();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _sprite = new Sprite2D(Content.Load<Texture2D>("dva"));
            _sprite.Scale = 0.4f;

            foreach (List<SpaceCraftModule> a in playerSpaceCraft.Modules)
                foreach (SpaceCraftModule b in a)
                    Debug.WriteLine(b.ToString());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _sprite.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}