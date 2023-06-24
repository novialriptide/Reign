using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Models
{
    public class SpaceCraftModule
    {
        public virtual string Name { get; }
        public virtual string Description { get; }
        public virtual int Width { get; }
        public virtual int Height { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
