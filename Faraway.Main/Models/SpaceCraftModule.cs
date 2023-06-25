﻿using System;
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
        public virtual int Width { get => 1; }
        public virtual int Height { get => 1; }

        public override string ToString()
        {
            return Name;
        }
    }
}
