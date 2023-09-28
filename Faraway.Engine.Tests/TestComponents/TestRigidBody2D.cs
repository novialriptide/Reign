using Faraway.Engine.Components;

namespace Faraway.Engine.Tests
{
    [TestClass]
    public class TestRigidBody2D
    {
        [TestMethod]
        public void TestInitSuccess()
        {
            TestSceneBody scene = new TestSceneBody();
            RigidBody2D rb1 = scene.Obj1.GetComponent<RigidBody2D>();

            Assert.IsNull(rb1.Body);
            scene.OnStart();

            Assert.IsNotNull(rb1.Body);
            Assert.IsNotNull(rb1.Simulation);

            scene.OnDestroy();
        }
    }

    internal class TestObjectBody : GameObject
    {
        private BoxCollider2D collider;

        public TestObjectBody()
        {
            AddComponent(new Transform());
            AddComponent(collider = new BoxCollider2D());
            AddComponent(new RigidBody2D());
            collider.Size.X = 150;
            collider.Size.Y = 150;
            base.OnAdd();
        }
    }
    internal class TestSceneBody : Scene
    {
        public TestObjectBody Obj1 = new TestObjectBody();
        public TestObjectBody Obj2 = new TestObjectBody();
        public override void OnStart()
        {
            AddGameObject(Obj1);
            AddGameObject(Obj2);
            base.OnStart();
        }
    }
}
