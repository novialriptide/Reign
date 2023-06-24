using Faraway.Main.Engine;
using Faraway.Main.Models.SpaceCraftModules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Faraway.Main.Models
{
    public class SpaceCraft
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
            return true;
        }
        public SpaceCraftModule GetModule(int x, int y)
        {
            return null;
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
                Rectangle rectangle = new Rectangle((int)Position.X, (int)Position.Y, ModulePixelWidth, ModulePixelHeight);
                Engine.Draw.DrawRectangle(spriteBatch, rectangle, Color.Blue);
            }
        }
    }
}
