using System.Numerics;
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
        public WorldPlayerInput WorldPlayerInput;
        public PlayerInputHandler()
        {
            AddComponent(Transform = new Transform());
            AddComponent(DragSelect = new DragSelect());
            AddComponent(WorldPlayerInput = new WorldPlayerInput());
            AddComponent(RectangleRenderer = new RectangleRenderer());
            AddComponent(BoxCollider2D = new BoxCollider2D(new Vector2(0, 0)));
        }
    }
}
