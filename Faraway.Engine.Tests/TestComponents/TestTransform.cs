using System.Numerics;
using System.Threading.Tasks.Dataflow;
using Faraway.Engine.Components;

namespace Faraway.Engine.Tests.TestComonents
{
    [TestClass]
    public class TestTransform
    {
        [TestMethod]
        public void TestEnabled()
        {
            TestSceneTransform scene = new TestSceneTransform();
            scene.OnStart();

            Transform transform1 = scene.Obj1.GetComponent<Transform>();
            Transform transform2 = scene.Obj2.GetComponent<Transform>();

            Assert.IsTrue(transform1.IsEnabled);
            Assert.IsTrue(transform2.IsEnabled);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestPosition()
        {
            TestSceneTransform scene = new TestSceneTransform();
            scene.OnStart();

            Transform transform1 = scene.Obj1.GetComponent<Transform>();
            Transform transform2 = scene.Obj2.GetComponent<Transform>();

            Assert.AreEqual(transform1.Position, new Vector2());
            Assert.AreEqual(transform2.Position, new Vector2());

            transform1.Position = new Vector2(12405, 5430);
            Assert.AreEqual(transform1.Position, new Vector2(12405, 5430));
            Assert.AreEqual(transform1.WorldPosition, new Vector2(12405, 5430));

            transform2.Position = new Vector2(-4512405, -5302935);
            Assert.AreEqual(transform2.Position, new Vector2(-4512405, -5302935));
            Assert.AreEqual(transform2.WorldPosition, new Vector2(-4512405, -5302935));

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestRotation()
        {
            TestSceneTransform scene = new TestSceneTransform();
            scene.OnStart();

            Transform transform1 = scene.Obj1.GetComponent<Transform>();

            transform1.Rotation = 547246f;
            Assert.AreEqual(transform1.Rotation, 547246f);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestNoParentChildRelation()
        {
            TestSceneTransform scene = new TestSceneTransform();
            scene.OnStart();

            Transform transform1 = scene.Obj1.GetComponent<Transform>();
            Assert.AreEqual(transform1.Children.Length, 0);
            Assert.AreEqual(transform1.Parent, null);
        }
        [TestMethod]
        public void TestSetParent()
        {
            TestSceneTransform scene = new TestSceneTransform();
            scene.OnStart();

            Transform transform1 = scene.Obj1.GetComponent<Transform>();
            Transform transform2 = scene.Obj2.GetComponent<Transform>();
            transform1.Parent = transform2;
            Assert.IsTrue(transform2.IsParentOf(transform1));
            Assert.IsTrue(transform1.IsChildOf(transform2));
            Assert.AreEqual(transform1.Parent, transform2);
            Assert.AreEqual(transform2.Children[0], transform1);
            Assert.AreEqual(transform2.Children.Length, 1);
            Assert.AreEqual(transform1.Children.Length, 0);
            Assert.IsTrue(transform2.Children.Contains(transform1));
            Assert.IsTrue(transform1.IsChild);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestChildPosition()
        {
            TestSceneTransform scene = new TestSceneTransform();
            scene.OnStart();

            Transform transform1 = scene.Obj1.GetComponent<Transform>();
            Transform transform2 = scene.Obj2.GetComponent<Transform>();
            transform1.Parent = transform2;

            Assert.AreEqual(transform1.WorldPosition, Vector2.Zero);

            transform2.Position = new Vector2(2350, 1521);
            Assert.AreEqual(transform1.WorldPosition, new Vector2(2350, 1521));
            Assert.AreEqual(transform1.Position, new Vector2(0, 0));

            transform1.Position = new Vector2(3529, -1250);
            Assert.AreEqual(transform1.WorldPosition, new Vector2(2350 + 3529, 1521 - 1250));
            Assert.AreEqual(transform1.Position, new Vector2(3529, -1250));

            transform1.Parent = null;
            Assert.AreEqual(transform2.Position, new Vector2(2350, 1521));
            Assert.AreEqual(transform1.Position, new Vector2(3529, -1250));

            scene.OnDestroy();
        }
    }

    internal class TestObjectTransform : GameObject
    {
        public TestObjectTransform()
        {
            AddComponent(new Transform());
            base.OnAdd();
        }
    }
    internal class TestSceneTransform : Scene
    {
        public TestObjectTransform Obj1 = new TestObjectTransform();
        public TestObjectTransform Obj2 = new TestObjectTransform();
        public override void OnStart()
        {
            AddGameObject(Obj1);
            AddGameObject(Obj2);
            base.OnStart();
        }
    }
}
