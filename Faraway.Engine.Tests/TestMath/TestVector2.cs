using System.Numerics;
using Faraway.Engine.MathExtended;

namespace Faraway.Engine.Tests.TestMath
{
    [TestClass]
    public class TestVector2
    {
        [TestMethod]
        public void TestMagnitudeInteger()
        {
            var a = new Vector2(5, 5);
            Assert.AreEqual(MathV.Magnitude(a), 7.071067810058594);
        }
        [TestMethod]
        public void TestMagnitudeFloat()
        {
            var a = new Vector2(756.2f, 24.6f);
            Assert.AreEqual(MathV.Magnitude(a), 756.6, 1);
        }
        [TestMethod]
        public void TestSetMagnitudeInteger()
        {
            var a = new Vector2(5, 5);
            a = MathV.SetMagnitude(a, 1);
            Assert.AreEqual(MathV.Magnitude(a), 1);

            a = MathV.SetMagnitude(a, 765);
            Assert.AreEqual(MathV.Magnitude(a), 765, 1);
        }
        [TestMethod]
        public void TestSetMagnitudeFloat()
        {
            var a = new Vector2(2346754.43f, 346.24f);
            a = MathV.SetMagnitude(a, 1);
            Assert.AreEqual(MathV.Magnitude(a), 1);

            a = MathV.SetMagnitude(a, 4.25f);
            Assert.AreEqual(MathV.Magnitude(a), 4.25f, 2);
        }
    }
}
