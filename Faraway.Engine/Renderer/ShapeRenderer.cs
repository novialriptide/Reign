using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Engine.Renderer
{
    /// <summary>
    /// The goal of this module is to provide rendering methods that aren't avaliable from MonoGame
    /// out of the box.
    /// </summary>
    public class ReignBatch
    {
        private BasicEffect basicEffect;

        public ReignBatch(GraphicsDevice graphicsDevice)
        {
            basicEffect = new BasicEffect(graphicsDevice);
        }
        public GraphicsDevice GraphicsDevice => basicEffect.GraphicsDevice;

        public void Draw(GameTime gameTime)
        {
            
        }
    }
}
