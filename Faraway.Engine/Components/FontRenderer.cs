using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Engine.Components
{
    public sealed class FontRenderer : Component
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
