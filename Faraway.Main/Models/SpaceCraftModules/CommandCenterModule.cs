using Faraway.Main.Engine.Components;

namespace Faraway.Main.Models.SpaceCraftModules
{
    public class CommandCenterModule : SpaceCraftModule
    {
        public override string Name => "Command Center";
        public override string Description => "The core module of your space craft. If this module dies, then your entire space craft becomes inoperable.";
        public CommandCenterModule()
        {
            Transform = new Transform();
            Sprite2D = new Sprite2D();
        }
    }
}
