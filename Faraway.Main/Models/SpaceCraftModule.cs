using Faraway.Main.Engine;
using Faraway.Main.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Models
{
    public abstract class SpaceCraftModule : GameObject
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual int Width { get => 1; }
        public virtual int Height { get => 1; }
        public Transform Transform;
        public Sprite2D Sprite2D;

        public SpaceCraftModule()
        {
            Transform = AddComponent<Transform>(new Transform());
            Sprite2D = AddComponent<Sprite2D>(new Sprite2D());
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
