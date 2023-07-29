using System.Collections.Generic;
using Faraway.Main.GameObjects.SpaceCraftModules;
using Faraway.Main.Models.Items;

namespace Faraway.Main.Models
{
    public class Inventory
    {
        public Player Player;
        public int MaxItems
        {
            get
            {
                int value = 0;
                foreach (StorageModule item in Player.GetModules<StorageModule>())
                    value += item.StorageCapacity;

                return value;
            }
        }
        public List<ObtainableItem> Items;

        public Inventory(Player player)
        {
            Player = player;
            player.Inventory = this;
        }
    }
}
