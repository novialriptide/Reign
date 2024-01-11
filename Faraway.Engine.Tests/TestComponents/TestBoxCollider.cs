using System.Numerics;
using Faraway.Engine.Components;

namespace Faraway.Engine.Tests.TestComponents
{
    /// <summary>
    /// This is heavily based on <see href="https://github.com/tainicom/Aether.Physics2D">Aether.Physics2D</see>. If
    /// the physics engine were to change, the test cases would need to be heavily modified.
    /// </summary>
    [TestClass]
    public class TestBoxCollider
    {
        private class ObjectCollider : GameObject
        {
            private BoxCollider2D collider;

            public ObjectCollider()
            {
                AddComponent(new Transform());
                AddComponent(collider = new BoxCollider2D(new Vector2(150, 150)));
                base.OnAdd();
            }
        }
        private class SceneCollider : Scene
        {
            public ObjectCollider Obj1 = new ObjectCollider();
            public ObjectCollider Obj2 = new ObjectCollider();
            public override void OnStart()
            {
                AddGameObject(Obj1);
                AddGameObject(Obj2);
                base.OnStart();
            }
        }
        private class EmptyScene : Scene { }

        [Ignore]
        [TestMethod]
        public void TestCollidesWith()
        {
            SceneCollider scene = new SceneCollider();
            scene.OnStart();
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
        [TestMethod]
        public void TestFixureExists()
        {
            SceneCollider scene = new SceneCollider();
            scene.OnStart();
            BoxCollider2D collider1 = scene.Obj1.GetComponent<BoxCollider2D>();

            Assert.IsNotNull(collider1.Fixture);

            scene.OnDestroy();
        }

        private class DetectionComponent : Component
        {
            public int OnCollisionEnterCalledTimes = 0;
            public int OnCollisionWhileCalledTimes = 0;
            public int OnCollisionExitCalledTimes = 0;

            public override void OnCollisionEnter(CollisionData collisionData)
            {
                OnCollisionEnterCalledTimes += 1;
                base.OnCollisionEnter(collisionData);
            }
            public override void OnCollisionWhile(CollisionData collisionData)
            {
                OnCollisionWhileCalledTimes += 1;
                base.OnCollisionWhile(collisionData);
            }
            public override void OnCollisionExit(CollisionData collisionData)
            {
                OnCollisionExitCalledTimes += 1;
                base.OnCollisionExit(collisionData);
            }
        }
        private class DummyObjectWithCollisionDetectors : GameObject
        {
            private BoxCollider2D collider;
            private DetectionComponent detector;

            public DummyObjectWithCollisionDetectors(bool hasRigidBody2D)
            {
                AddComponent(new Transform());
                AddComponent(collider = new BoxCollider2D(new Vector2(150, 150)));
                if (hasRigidBody2D)
                    AddComponent(new RigidBody2D());
                AddComponent(detector = new DetectionComponent());
                base.OnAdd();
            }
        }

        /// <summary>
        /// Just like how women flirt to get what they want, the objects
        /// will flirt with each other while testing.
        /// 
        /// TODO: Make this pass without needing a RigidBody2D.
        /// </summary>
        [TestMethod]
        public void TestOnCollisionEnter()
        {
            EmptyScene scene = new EmptyScene();
            DummyObjectWithCollisionDetectors o1;
            DummyObjectWithCollisionDetectors o2;
            scene.OnStart();

            scene.AddGameObject(o1 = new DummyObjectWithCollisionDetectors(false));
            DetectionComponent o1_c = o1.GetComponent<DetectionComponent>();

            Assert.AreEqual(0, o1_c.OnCollisionEnterCalledTimes);

            scene.Step(0);

            Assert.AreEqual(0, o1_c.OnCollisionEnterCalledTimes);

            scene.AddGameObject(o2 = new DummyObjectWithCollisionDetectors(false));
            DetectionComponent o2_c = o2.GetComponent<DetectionComponent>();

            Assert.AreEqual(0, o1_c.OnCollisionEnterCalledTimes);
            Assert.AreEqual(0, o2_c.OnCollisionEnterCalledTimes);
            scene.Step(0);
            Assert.AreEqual(1, o1_c.OnCollisionEnterCalledTimes);
            Assert.AreEqual(1, o2_c.OnCollisionEnterCalledTimes);
            scene.Step(0);
            Assert.AreEqual(1, o1_c.OnCollisionEnterCalledTimes);
            Assert.AreEqual(1, o2_c.OnCollisionEnterCalledTimes);

            scene.OnDestroy();
        }
        [Ignore]
        [TestMethod]
        public void TestOnCollisionWhile()
        {

        }
        [Ignore]
        [TestMethod]
        public void TestOnCollisionExit()
        {

        }
    }
}
