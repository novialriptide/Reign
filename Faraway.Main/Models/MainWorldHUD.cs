using Faraway.Main.Engine;
using Faraway.Main.Engine.UI;

namespace Faraway.Main.Models
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
