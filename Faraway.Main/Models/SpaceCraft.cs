using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Models
{
    public class SpaceCraft
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<List<SpaceCraftIntegratedModule>> Modules { get; }
        public SpaceCraft()
        {
            
        }

        //https://stackoverflow.com/questions/7200780/how-to-use-2d-arrays-with-negative-index
        private int getUnsignedIndex(int index)
        {
            if (index < 0)
                return -index * 2 - 1;
            else
                return index * 2;
        }
        private int getSignedIndex(int index)
        {
            if (index == 0)
                return 0;
            else if (index % 2 == 0)
                return index % 2;
            else if ((index % 2) == 1)
                return index % 2 + 1;
        }
        private bool IsValidInsertionPosition(int x, int y)
        {
            return false;
        }
        public void InsertModule(SpaceCraftIntegratedModule module, int x, int y)
        {
            if (x == 0 && y == 0)
                return;

            x = getUnsignedIndex(x);
            y = getUnsignedIndex(y);

            Modules[y][x] = module;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
