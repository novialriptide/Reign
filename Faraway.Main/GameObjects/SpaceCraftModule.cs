using System.Numerics;
using Faraway.Engine;
using Faraway.Engine.Components;
using Faraway.Main.Components.UserInterface;

namespace Faraway.Main.GameObjects
{
    public abstract class SpaceCraftModule : GameObject
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual int Width => 1;
        public virtual int Height => 1;
        public Transform Transform;
        public BoxCollider2D BoxCollider2D;
        public SpriteRenderer Sprite2D;

        public SpaceCraftModule()
        {
            AddComponent(Transform = new Transform());
            AddComponent(Sprite2D = new SpriteRenderer());
            AddComponent(BoxCollider2D = new BoxCollider2D(new Vector2(SpaceCraft.ModulePixelWidth, SpaceCraft.ModulePixelHeight)));
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
