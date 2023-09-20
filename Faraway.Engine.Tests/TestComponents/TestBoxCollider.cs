using System.Numerics;
using Faraway.Engine.Components;

namespace Faraway.Engine.Tests.TestComonents
{
    [TestClass]
    public class TestBoxCollider
    {
        [TestMethod]
        public void TestCollidesWith()
        {
            TestSceneBoxCollider scene = new TestSceneBoxCollider();
            scene.OnStart();
            _ = scene.Obj1.GetComponent<Transform>();
            BoxCollider2D collider1 = scene.Obj1.GetComponent<BoxCollider2D>();
            Transform transform2 = scene.Obj2.GetComponent<Transform>();
            BoxCollider2D collider2 = scene.Obj2.GetComponent<BoxCollider2D>();
            Assert.IsTrue(collider1.CollidesWith(collider2));

            collider1.IsEnabled = false;
            collider2.IsEnabled = true;

            Assert.IsFalse(collider1.CollidesWith(collider2));

            collider1.IsEnabled = true;
            collider2.IsEnabled = false;

            Assert.IsFalse(collider1.CollidesWith(collider2));

            transform2.Position = new Vector2(0, 4000);
            Assert.IsFalse(collider1.CollidesWith(collider2));

            scene.OnDestroy();
        }
    }

    internal class TestObjectCollider : GameObject
    {
        private BoxCollider2D collider;

        public TestObjectCollider()
        {
            AddComponent(new Transform());
            AddComponent(collider = new BoxCollider2D());
            collider.Size.X = 150;
            collider.Size.Y = 150;
            base.OnAdd();
        }
    }
    internal class TestSceneBoxCollider : Scene
    {
        public TestObjectCollider Obj1 = new TestObjectCollider();
        public TestObjectCollider Obj2 = new TestObjectCollider();
        public override void OnStart()
        {
            AddGameObject(Obj1);
            AddGameObject(Obj2);
            base.OnStart();
        }
    }
}
