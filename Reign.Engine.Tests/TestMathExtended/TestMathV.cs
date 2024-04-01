using System.Numerics;
using Reign.Engine.MathExtended;

namespace Reign.Engine.Tests.TestMathExtended
{
    [TestClass]
    public class TestMathV
    {
        [TestMethod]
        public void TestMagnitudeInteger()
        {
            var a = new Vector2(5, 5);
            Assert.AreEqual(a.GetMagnitude(), 7.071067810058594);
        }
        [TestMethod]
        public void TestMagnitudeFloat()
        {
            var a = new Vector2(756.2f, 24.6f);
            Assert.AreEqual(a.GetMagnitude(), 756.6, 1);
        }
        [TestMethod]
        public void TestSetMagnitudeInteger()
        {
            var a = new Vector2(5, 5);
            a = a.SetMagnitude(1);
            Assert.AreEqual(MathV.GetMagnitude(a), 1);

            a = a.SetMagnitude(765);
            Assert.AreEqual(MathV.GetMagnitude(a), 765, 1);
        }
        [TestMethod]
        public void TestSetMagnitudeFloat()
        {
            var a = new Vector2(2346754.43f, 346.24f);
            a = a.SetMagnitude(1);
            Assert.AreEqual(a.GetMagnitude(), 1);

            a = a.SetMagnitude(4.25f);
            Assert.AreEqual(a.GetMagnitude(), 4.25f, 2);
        }
    }
}
