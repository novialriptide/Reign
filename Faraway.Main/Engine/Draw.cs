using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Main.Engine
{
    public static class Draw
    {
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            Texture2D whiteRectangle = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            spriteBatch.Draw(whiteRectangle, rectangle, color);
        }
    }
}
