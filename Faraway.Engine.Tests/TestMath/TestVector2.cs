using System.Numerics;

namespace Faraway.Engine.Tests.TestMath
{
    [TestClass]
    public class TestVector2
    {
        [TestMethod]
        public void TestMagnitudeInteger()
        {
            var a = new Vector2(5, 5);
            Assert.AreEqual(Math.Magnitude(a), 7.071067810058594);
        }
        public void TestMagnitudeFloat()
        {
            var a = new Vector2(756.2f, 24.6f);
            Assert.AreEqual(Math.Magnitude(a), 756.60004);
        }
    }

}
