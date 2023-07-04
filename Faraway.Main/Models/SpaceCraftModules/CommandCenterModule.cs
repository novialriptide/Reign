using Faraway.Main.Engine;
using Faraway.Main.Engine.Components;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Faraway.Main.Models.SpaceCraftModules
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
