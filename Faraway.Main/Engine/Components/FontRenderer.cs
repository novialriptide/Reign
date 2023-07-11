using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Main.Engine.Components
{
    public class FontRenderer : Component
    {
        public SpriteFont SpriteFont;
        public string Text = "Hana Song is the Best";
        public Color Color = Color.Black;

        public FontRenderer() { }
        public FontRenderer(SpriteFont spriteFont)
        {
            SpriteFont = spriteFont;
        }
    }
}
