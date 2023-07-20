using System.Diagnostics;
using Faraway.Engine;
using Faraway.Main.Components.SpaceCraftModules;
using Faraway.Main.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faraway.Main.Scenes
{
    public class MainWorld : Scene
    {
        private SpaceCraft playerSpaceCraft;

        public override void OnStart()
        {
            AddGameObject(playerSpaceCraft = new SpaceCraft());
            playerSpaceCraft.SetModule(0, 1, new IonTurret());
            base.OnStart();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                GameInstance.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                playerSpaceCraft.Transform.Position.X += 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            GameInstance.Window.Title = (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
    }
}
