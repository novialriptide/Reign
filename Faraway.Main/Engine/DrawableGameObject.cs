using Faraway.Main.Engine.Components;
using System.Collections.Generic

namespace Faraway.Main.Engine
{
    public abstract class DrawableGameObject : GameObject
    {
        public Transform Transform { get; private set; }
        public Sprite2D Sprite2D { get; private set; }
    }
}
