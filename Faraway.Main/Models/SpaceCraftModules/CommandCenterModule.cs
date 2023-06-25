using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Models.SpaceCraftModules
{
    public class CommandCenterModule : SpaceCraftModule
    {
        public override string Name => "Command Center";
        public override string Description => "The core module of your space craft. If this module dies, then your entire space craft becomes inoperable.";
    }
}
