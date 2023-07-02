using Faraway.Main.Engine;
using Faraway.Main.Engine.Components;
using Faraway.Main.Models.SpaceCraftModules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

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

        public Sprite2D Sprite2D;

        public SpaceCraft()
        {
            Modules = new Dictionary<(int x, int y), SpaceCraftModule>();
        }

        public override void OnAdd()
        {
            CommandCenterModule commandCenter = new CommandCenterModule();
            Scene.AddGameObject<CommandCenterModule>(commandCenter);
            SetModule(0, 0, commandCenter);

            base.OnAdd();
        }

        /// <summary>
        /// Returns <c>true</c> if placing a module at <c>x</c> and <c>y</c> is valid.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsValidInsertionPosition(int x, int y)
        {
            if (Modules.ContainsKey((x, y)))
                return false;

            // TODO: Add handler if the module would be a potential neighbor.

            return true;
        }
        /// <summary>
        /// Returns a SpaceCraftModule object, returns null if it doesn't exist.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public SpaceCraftModule GetModule(int x, int y)
        {
            if (!Modules.ContainsKey((x, y)))
                return null;

            return Modules[(x, y)];
        }
        /// <summary>
        /// Sets a SpaceCraftModule at the specified 
        /// coordinates, does nothing if position is invalid.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="module"></param>
        public void SetModule(int x, int y, SpaceCraftModule module)
        {
            if (!IsValidInsertionPosition(x, y))
                return;

            Modules.Add((x, y), module);
        }
    }
}
