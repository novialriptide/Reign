using Faraway.Main.Models.SpaceCraftModules;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Models
{
    public class SpaceCraft
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<List<SpaceCraftModule>> Modules { get; }
        public SpaceCraft()
        {
            Modules = new List<List<SpaceCraftModule>>();
            Modules.Add(new List<SpaceCraftModule>());
            SetModule(0, 0, new CommandCenterModule());
        }

        //https://stackoverflow.com/questions/7200780/how-to-use-2d-arrays-with-negative-index
        private int getUnsignedIndex(int index)
        {
            if (index < 0)
                return -index * 2 - 1;
            
            return index * 2;
        }
        private int getSignedIndex(int index)
        {
            if (index == 0)
                return 0;
            else if (index % 2 == 0)
                return index % 2;
            
            return index % 2 + 1;
        }
        private bool IsValidInsertionPosition(int x, int y)
        {
            return true;
        }
        public SpaceCraftModule GetModule(int x, int y)
        {
            x = getUnsignedIndex(x);
            y = getUnsignedIndex(y);

            return Modules[y][x];
        }
        public void SetModule(int x, int y, SpaceCraftModule module)
        {
            x = getUnsignedIndex(x);
            y = getUnsignedIndex(y);

            if (!IsValidInsertionPosition(x, y))
            {
                return;
            }

            if (y - Modules.Count > 0)
                for (int iy = 0; iy < y - Modules.Count; iy++)
                    Modules.Add(new List<SpaceCraftModule>());

            if (x - Modules[0].Count > 0)
                for (int ix = 0; ix < x - Modules.Count; ix++)
                    Modules.Add(null);

            Modules[y][x] = module;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
