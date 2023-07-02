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
        Texture2D baseModule;


        private SpaceCraft playerSpaceCraft;
        private CommandCenterModule testModule;

        public MainWorld()
        {
            playerSpaceCraft = AddGameObject<SpaceCraft>(new SpaceCraft());
            testModule = AddGameObject<CommandCenterModule>(new CommandCenterModule());
        }

        public override void OnStart()
        {
            testModule.GetComponent<Sprite2D>().texture = GameInstance.Content.Load<Texture2D>("baseModule");

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
