using Microsoft.Xna.Framework;

namespace Faraway.Engine.Tests.UniversalObjects
{
    public class TestGame : GameInstance
    {
        public TestGame()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
        }
    }
}
