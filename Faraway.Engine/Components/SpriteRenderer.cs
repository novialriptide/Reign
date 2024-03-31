using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Engine.Components
{
    public sealed class SpriteRenderer : Component
    {
        public Texture2D Texture;
        public bool FlipX = false;
        public bool FlipY = false;
        public SpriteRenderer() { }
        public void LoadTexureFromFile(string path)
        {
            GraphicsDevice graphicsDevice = GameObject.Scene.GameInstance.GraphicsDevice;
            // TODO: Make this not `Faraway.Main`
            Stream stream = new FileStream($"Faraway.Main/Content/{path}", FileMode.Open);
            Texture = Texture2D.FromStream(graphicsDevice, stream);
        }
    }
}
