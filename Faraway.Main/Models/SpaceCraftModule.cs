using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Models
{
    public abstract class SpaceCraftModule
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract int Width { get; }
        public abstract int Height { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
