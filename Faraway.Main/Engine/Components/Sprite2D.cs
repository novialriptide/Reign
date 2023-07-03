using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Main.Engine.Components
{
    public class Sprite2D : Component
    {
        public Texture2D Texture;
        SpriteBatch spriteBatch;
        public Sprite2D() { }
        public Sprite2D(Texture2D texture)
        {
            this.Texture = texture;
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
