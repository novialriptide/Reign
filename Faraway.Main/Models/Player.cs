using System;
using System.Collections.Generic;
using Faraway.Main.GameObjects;

namespace Faraway.Main.Models
{
    public class Player
    {
        public Guid Guid => Guid.NewGuid();
        public string Name { get; }
        public Inventory Inventory;
        public Player(string name)
        {
            Name = name;
        }
        /// <summary>
        /// Get a list of modules that belong to this player.
        /// </summary>
        public List<SpaceCraftModule> GetModules<T>() where T : SpaceCraftModule
        {
            // TODO: Properly implement this.

            return new List<SpaceCraftModule>();
        }
    }
}
