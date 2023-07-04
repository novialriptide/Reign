using System.Collections.Generic;
using Faraway.Main.Engine;
using Faraway.Main.Engine.Components;
using Faraway.Main.Models.SpaceCraftModules;

namespace Faraway.Main.Models
{
    public class SpaceCraft : GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public static int ModulePixelWidth = 32;
        public static int ModulePixelHeight = 32;
        public Dictionary<(int x, int y), SpaceCraftModule> Modules { get; }

        public Transform Transform;

        public SpaceCraft()
        {
            Modules = new Dictionary<(int x, int y), SpaceCraftModule>();
        }
        /// <summary>
        /// Called when the object is added.
        /// </summary>
        public override void OnAdd()
        {
            CommandCenterModule commandCenter = new CommandCenterModule();
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

            Transform t = module.GetComponent<Transform>();
            t.Position.X += x * ModulePixelWidth;
            t.Position.Y += y * ModulePixelHeight;
            Scene.AddGameObject(module);
            Modules.Add((x, y), module);
        }
    }
}
