using Faraway.Engine;
using Faraway.Main.Components;

namespace Faraway.Main.GameObjects
{
    public class PlayerInputHandler : GameObject
    {
        public DragSelect DragSelect;
        public PlayerInputHandler()
        {
            AddComponent(DragSelect = new DragSelect());
        }
    }
}
