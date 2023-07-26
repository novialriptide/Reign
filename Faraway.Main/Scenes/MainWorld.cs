using Faraway.Engine;
using Faraway.Main.GameObjects;
using Faraway.Main.GameObjects.SpaceCraftModules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faraway.Main.Scenes
{
    public class MainWorld : Scene
    {
        private SpaceCraft playerSpaceCraft;
        private PlayerInputHandler inputHandler;

        public override void OnStart()
        {
            AddGameObject(playerSpaceCraft = new SpaceCraft());
            playerSpaceCraft.SetModule(0, 1, new IonTurret());

            AddGameObject(inputHandler = new PlayerInputHandler());

            base.OnStart();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                GameInstance.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                playerSpaceCraft.Transform.Position.X += 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            GameInstance.Window.Title = (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString();

            base.Update(gameTime);
        }
    }
}
