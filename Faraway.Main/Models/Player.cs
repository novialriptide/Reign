using System;

namespace Faraway.Main.Models
{
    public class Player
    {
        public Guid Guid => Guid.NewGuid();
        public string Name { get; }
        public Player(string name)
        {
            Name = name;
        }
    }
}
