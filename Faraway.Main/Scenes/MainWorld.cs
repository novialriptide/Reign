using Faraway.Main.Engine;
using Faraway.Main.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Faraway.Main.Scenes
{
    public class MainWorld : Scene
    {

        private SpaceCraft playerSpaceCraft;
        public MainWorld()
        {
            playerSpaceCraft = new SpaceCraft();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                GameInstance.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameInstance.GraphicsDevice.Clear(Color.CornflowerBlue);

            GameInstance.SpriteBatch.Begin();
            playerSpaceCraft.Draw(GameInstance.SpriteBatch);
            GameInstance.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
