using System.Numerics;
using Faraway.Engine.Components;

namespace Faraway.Engine.Tests.TestComonents
{
    [TestClass]
    public class TestTransform
    {
        [TestMethod]
        public void TestEnabled()
        {
            TestSceneBoxCollider scene = new TestSceneBoxCollider();
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
            TestSceneBoxCollider scene = new TestSceneBoxCollider();
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
            TestSceneBoxCollider scene = new TestSceneBoxCollider();
            scene.OnStart();

            Transform transform1 = scene.Obj1.GetComponent<Transform>();

            transform1.Rotation = 547246f;
            Assert.AreEqual(transform1.Rotation, 547246f);

            scene.OnDestroy();
        }
    }

    internal class TestObjectTransform : GameObject
    {
        private Transform transform;

        public TestObjectTransform()
        {
            AddComponent(transform = new Transform());
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
