using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Faraway.Main.Engine
{
    public class Sprite2D
    {
        public Vector2 Position;
        public Vector2 RotationOrigin;
        public float Scale;
        public float Rotation;

        private Texture2D _texture2D;

        public Sprite2D(Texture2D texture2D)
        {
            _texture2D = texture2D;
            Rotation = 0f;
            Scale = 1f;
            RotationOrigin = Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture2D, Position, null, Color.White,
                Rotation, RotationOrigin, Scale, SpriteEffects.None, 0f);
        }
    }
}
