using Faraway.Engine;
using Faraway.Engine.Components;
using Faraway.Main.Components.UserInterface;

namespace Faraway.Main.GameObjects
{
    public class PlayerInputHandler : GameObject
    {
        public Transform Transform;
        public DragSelect DragSelect;
        public RectangleRenderer RectangleRenderer;
        public BoxCollider2D BoxCollider2D;
        public PlayerInputHandler()
        {
            AddComponent(Transform = new Transform());
            AddComponent(DragSelect = new DragSelect());
            AddComponent(RectangleRenderer = new RectangleRenderer());
            AddComponent(BoxCollider2D = new BoxCollider2D());
        }
    }
}
