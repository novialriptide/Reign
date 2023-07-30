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
        /// <summary>
        /// To register spacecrafts use <c>RegisterSpaceCraft()</c>
        /// </summary>
        private readonly List<SpaceCraft> spaceCrafts = new List<SpaceCraft>();
        public Player(string name)
        {
            Name = name;
            Inventory = new Inventory(this);
        }
        /// <summary>
        /// Registers the specified spacecraft to the player.
        /// </summary>
        /// <param name="player"></param>
        public void RegisterSpaceCraft(SpaceCraft spaceCraft)
        {
            // Check if the spacecraft is already owned by a different player.
            if (spaceCraft.Owner != null && spaceCraft.Owner.spaceCrafts.Contains(spaceCraft))
                spaceCraft.Owner.spaceCrafts.Remove(spaceCraft);

            spaceCraft.Owner = this;
            spaceCrafts.Add(spaceCraft);
        }
        /// <summary>
        /// Get a list of modules that belong to this player.
        /// </summary>
        public List<SpaceCraftModule> GetModules<T>() where T : SpaceCraftModule
        {
            List<SpaceCraftModule> value = new List<SpaceCraftModule>();
            foreach (SpaceCraft spaceCraft in spaceCrafts)
                foreach (var module in spaceCraft.Modules)
                    if (module.GetType() == typeof(T))
                        value.Add(module);

            return value;
        }
    }
}
