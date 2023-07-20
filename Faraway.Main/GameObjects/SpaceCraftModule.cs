using Faraway.Engine;
using Faraway.Engine.Components;

namespace Faraway.Main.GameObjects
{
    public abstract class SpaceCraftModule : GameObject
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual int Width => 1;
        public virtual int Height => 1;
        public Transform Transform;
        public SpriteRenderer Sprite2D;

        public SpaceCraftModule()
        {
            AddComponent(Transform = new Transform());
            AddComponent(Sprite2D = new SpriteRenderer());
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
