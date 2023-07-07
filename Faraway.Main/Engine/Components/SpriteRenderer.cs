using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Main.Engine.Components
{
    public class SpriteRenderer : Component
    {
        public Texture2D Texture;
        private SpriteBatch spriteBatch;
        public SpriteRenderer() { }
        public SpriteRenderer(Texture2D texture)
        {
            Texture = texture;
        }
        public override void Update(float deltaTime)
        {
            Transform t = GameObject.GetComponent<Transform>();

            spriteBatch.Draw(
            Texture, t.Position, null, Color.White,
                t.Rotation, t.RotationOrigin, t.Scale, SpriteEffects.None, 0f);

            base.Update(deltaTime);
        }
    }
}
