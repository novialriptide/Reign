using Faraway.Engine;
using Faraway.Engine.UI;

namespace Faraway.Main.Components
{
    public class MainWorldHUD : GameObject
    {
        private UICanvas canvas;

        public override void OnAdd()
        {
            AddComponent(canvas = new UICanvas());


            canvas.Content = new UIContainer[] {

            };

            base.OnAdd();
        }
    }
}
