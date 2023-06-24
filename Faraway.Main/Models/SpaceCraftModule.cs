using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Models
{
    public class SpaceCraftModule
    {
        public string Name { get; }
        public string Description { get; }
        public int Width { get; }
        public int Height { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
