using System.Numerics;
using Faraway.Engine.Components;
using Faraway.Engine.MathExtended;

namespace Faraway.Engine.Tests.TestMathExtended
{
    [TestClass]
    public class TestAlgorithms
    {
        [TestMethod]
        public void TestFindMissingComponents1()
        {
            BoxCollider2D obj1 = new BoxCollider2D(new Vector2(0, 0));
            BoxCollider2D obj2 = new BoxCollider2D(new Vector2(0, 0));
            BoxCollider2D obj3 = new BoxCollider2D(new Vector2(0, 0));

            List<BoxCollider2D> original = new List<BoxCollider2D>() { obj1, obj2 };
            List<BoxCollider2D> modified = new List<BoxCollider2D>() { obj3 };
            List<BoxCollider2D> result = Algorithms.FindMissingComponents(original, modified);
            Assert.IsTrue(result.Contains(obj1));
            Assert.IsTrue(result.Contains(obj2));
            Assert.IsFalse(result.Contains(obj3));
        }
        [TestMethod]
        public void TestFindMissingComponentsWhenNoChange()
        {
            BoxCollider2D obj1 = new BoxCollider2D(new Vector2(0, 0));
            BoxCollider2D obj2 = new BoxCollider2D(new Vector2(0, 0));
            BoxCollider2D obj3 = new BoxCollider2D(new Vector2(0, 0));

            List<BoxCollider2D> original = new List<BoxCollider2D>() { obj1, obj2, obj3 };
            List<BoxCollider2D> modified = new List<BoxCollider2D>() { obj1, obj2, obj3 };
            List<BoxCollider2D> result = Algorithms.FindMissingComponents(original, modified);
            Assert.AreEqual(0, result.Count);
        }
    }
}
