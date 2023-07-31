using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Engine.Components
{
    public sealed class SpriteRenderer : Component
    {
        public Texture2D Texture;
        public bool FlipX = false;
        public bool FlipY = false;
        public SpriteRenderer() { }
        public SpriteRenderer(Texture2D texture)
        {
            Texture = texture;
        }
    }
}
