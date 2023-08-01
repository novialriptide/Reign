using Faraway.Engine.Components;
using Faraway.Engine.Input;
using Faraway.Main.GameObjects;

namespace Faraway.Main.Components.UserInterface
{
    public class WorldPlayerInput : Component
    {
        public DragSelect DragSelect;
        public override void Start()
        {
            DragSelect = GameObject.GetComponent<DragSelect>();

            base.Start();
        }
        public override void Update(double deltaTime)
        {
            if (DragSelect.SelectedObjects.Count > 0 && MouseInput.RightButton.IsClickedUp)
                foreach (SelectableObject obj in DragSelect.SelectedObjects)
                    ((SpaceCraft)obj.GameObject).SetWaypoint(MouseInput.MousePosition);

            base.Update(deltaTime);
        }
    }
}
