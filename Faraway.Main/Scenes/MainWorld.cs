using Faraway.Engine;
using Faraway.Main.GameObjects;
using Faraway.Main.GameObjects.SpaceCraftModules;
using Faraway.Main.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faraway.Main.Scenes
{
    public class MainWorld : Scene
    {
        private Player player = new Player("novial");
        private SpaceCraft playerSpaceCraft;

        public override void OnStart()
        {
            AddGameObject(playerSpaceCraft = new SpaceCraft(player));
            playerSpaceCraft.SetModule(0, 1, new IonTurret());
            player.RegisterSpaceCraft(playerSpaceCraft);

            AddGameObject(new PlayerInputHandler());

            base.OnStart();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                GameInstance.Exit();

            GameInstance.Window.Title = (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString();

            base.Update(gameTime);
        }
    }
}
