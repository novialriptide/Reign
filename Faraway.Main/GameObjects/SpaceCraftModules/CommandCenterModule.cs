using System.Diagnostics;
using Faraway.Engine;
using Faraway.Main.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Main.GameObjects.SpaceCraftModules
{
    public class CommandCenterModule : SpaceCraftModule
    {
        public override string Name => "Command Center";
        public override string Description => "The core module of your space craft. If this module dies, then your entire space craft becomes inoperable.";
        public override void OnAdd()
        {
            Sprite2D.Texture = Scene.GameInstance.Content.Load<Texture2D>("baseModule");
            base.OnAdd();
        }
    }
}
