using Faraway.Main.Engine;
using Faraway.Main.Engine.Components;

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
            AddComponent(Transform = new Transform());
            AddComponent(Sprite2D = new Sprite2D());
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
