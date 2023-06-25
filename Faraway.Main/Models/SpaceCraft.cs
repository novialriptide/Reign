using Faraway.Main.Engine;
using Faraway.Main.Models.SpaceCraftModules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Faraway.Main.Models
{
    public class SpaceCraft : GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Vector2 Position { get; set; }
        public static int ModulePixelWidth = 128;
        public static int ModulePixelHeight = 128;
        public Dictionary<(int x, int y), SpaceCraftModule> Modules { get; }
        public SpaceCraft()
        {
            Modules = new Dictionary<(int x, int y), SpaceCraftModule>();
            SetModule(0, 0, new CommandCenterModule());
        }
        private bool IsValidInsertionPosition(int x, int y)
        {
            if (Modules.ContainsKey((x, y)))
                return false;

            // TODO: Add handler if the module would be a potential neighbor.

            return true;
        }
        public SpaceCraftModule GetModule(int x, int y)
        {
            if (!Modules.ContainsKey((x, y)))
                return null;

            return Modules[(x, y)];
        }
        public void SetModule(int x, int y, SpaceCraftModule module)
        {
            if (!IsValidInsertionPosition(x, y))
                return;

            Modules.Add((x, y), module);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach ((int x, int y) a in Modules.Keys)
            {
                SpaceCraftModule module = Modules[a];
                Rectangle rectangle = new Rectangle((int)Position.X * ModulePixelWidth, (int)Position.Y * ModulePixelHeight, ModulePixelWidth, ModulePixelHeight);
                Engine.Draw.DrawRectangle(spriteBatch, rectangle, Color.Blue);
            }
        }
    }
}
