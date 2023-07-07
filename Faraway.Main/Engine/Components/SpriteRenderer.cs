using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Main.Engine.Components
{
    public class SpriteRenderer : Component
    {
        public Texture2D Texture;
        public SpriteRenderer() { }
        public SpriteRenderer(Texture2D texture)
        {
            Texture = texture;
        }
    }
}
