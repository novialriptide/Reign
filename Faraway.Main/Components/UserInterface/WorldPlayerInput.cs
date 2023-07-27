using Faraway.Engine.Components;

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
            // TODO: Implement selecting `SelectableObject` game objects.

            base.Update(deltaTime);
        }
    }
}
