using System;
using Faraway.Engine;
using Faraway.Engine.Components;
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
        private SpaceCraft playerSpaceCraft2;

        public override void OnStart()
        {
            AddGameObject(playerSpaceCraft = new SpaceCraft(player));
            playerSpaceCraft.SetModule(0, 1, new IonTurret());
            player.RegisterSpaceCraft(playerSpaceCraft);
            playerSpaceCraft.GetComponent<Transform>().Position = new System.Numerics.Vector2(60, 60);

            AddGameObject(playerSpaceCraft2 = new SpaceCraft(player));
            playerSpaceCraft2.SetModule(0, 1, new IonTurret());
            player.RegisterSpaceCraft(playerSpaceCraft);
            playerSpaceCraft2.GetComponent<Transform>().Position = new System.Numerics.Vector2(610, 60);

            AddGameObject(new PlayerInputHandler());

            base.OnStart();
        }

        public override void Update()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                GameInstance.Exit();

            float fps = MathF.Round((float)GameInstance.FramesPerSecond, 0);
            float dt = MathF.Round((float)GameInstance.DeltaTime, 5);
            GameInstance.Window.Title = $"{fps} fps ({dt} ms)";

            base.Update();
        }
    }
}
