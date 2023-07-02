using Faraway.Main.Engine;
using Faraway.Main.Engine.Components;
using Faraway.Main.Models;
using Faraway.Main.Models.SpaceCraftModules;
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
            playerSpaceCraft = AddGameObject<SpaceCraft>(new SpaceCraft());
        }

        public override void OnStart()
        {
            // CommandCenter -> GameInstance.Content.Load<Texture2D>("baseModule");

            base.OnStart();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                GameInstance.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
    }
}
