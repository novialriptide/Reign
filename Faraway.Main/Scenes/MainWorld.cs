using Faraway.Engine;
using Faraway.Main.Components;
using Faraway.Main.Components.SpaceCraftModules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faraway.Main.Scenes
{
    public class MainWorld : Scene
    {
        private SpaceCraft playerSpaceCraft;
        private MainWorldHUD hudObject;

        public override void OnStart()
        {
            AddGameObject(hudObject = new MainWorldHUD());

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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
    }
}
