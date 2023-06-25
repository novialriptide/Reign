using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;

namespace Faraway.Main.Engine.Components
{
    public class Sprite2D : Component
    {
        Texture2D texture;
        SpriteBatch spriteBatch;

        public override void Update(float deltaTime)
        {
            Transform t = GameObject.GetComponent<Transform>();

            spriteBatch.Draw(
            texture, t.Position, null, Color.White,
                t.Rotation, t.RotationOrigin, t.Scale, SpriteEffects.None, 0f);

            base.Update(deltaTime);
        }
    }
}
