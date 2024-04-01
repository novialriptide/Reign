using System.Numerics;
using Reign.Engine.Components;

namespace Reign.Engine.Tests.TestComponents
{
    [TestClass]
    public class TestRigidBody2D
    {
        private class ObjectBodyWithCollider : GameObject
        {
            private BoxCollider2D collider;

            public ObjectBodyWithCollider()
            {
                AddComponent(new Transform());
                AddComponent(collider = new BoxCollider2D(new Vector2(150, 150)));
                AddComponent(new RigidBody2D());
                base.OnAdd();
            }
        }
        private class ObjectBody : GameObject
        {
            public ObjectBody()
            {
                AddComponent(new Transform());
                AddComponent(new RigidBody2D());
                base.OnAdd();
            }
        }
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
        private class SceneBody : Scene
        {
            public ObjectBodyWithCollider Obj1 = new ObjectBodyWithCollider();
            public ObjectCollider Obj2 = new ObjectCollider();
            public ObjectBody Obj3 = new ObjectBody();
            public override void OnStart()
            {
                AddGameObject(Obj1);
                AddGameObject(Obj2);
                AddGameObject(Obj3);
                base.OnStart();
            }
        }

        [TestMethod]
        public void TestInitSuccessNoChildren()
        {
            SceneBody scene = new SceneBody();
            scene.OnStart();

            RigidBody2D rb1 = scene.Obj1.GetComponent<RigidBody2D>();
            scene.OnStart();
            scene.Update();

            Assert.IsTrue(rb1.IsEnabled);
            Assert.AreEqual(BodyType.Dynamic, rb1.BodyType);
            Assert.AreEqual(1, rb1.BoxCollider2Ds.Count);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestInitSuccessWithChildren()
        {
            SceneBody scene = new SceneBody();
            scene.OnStart();

            scene.Obj3.GetComponent<Transform>().Parent = scene.Obj1.GetComponent<Transform>();
            RigidBody2D rb1 = scene.Obj1.GetComponent<RigidBody2D>();

            scene.Step(0f);

            Assert.IsTrue(rb1.IsEnabled);
            Assert.AreEqual(BodyType.Dynamic, rb1.BodyType);
            Assert.AreEqual(1, rb1.BoxCollider2Ds.Count);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestApplyRotationVelocity()
        {
            SceneBody scene = new SceneBody();
            scene.OnStart();

            scene.Step(0f);

            RigidBody2D rb1 = scene.Obj1.GetComponent<RigidBody2D>();
            Transform transform1 = scene.Obj1.GetComponent<Transform>();

            Assert.AreEqual(0, rb1.AngularVelocity);

            rb1.ApplyAngularImpulse(200);

            Assert.AreNotEqual(0f, rb1.AngularVelocity);
            Assert.AreEqual(0f, transform1.Rotation);

            scene.Step(200);
            Assert.AreNotEqual(0f, transform1.Rotation);

            rb1.AngularVelocity = 0;

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestApplyVelocity()
        {
            SceneBody scene = new SceneBody();
            scene.OnStart();

            RigidBody2D rb1 = scene.Obj1.GetComponent<RigidBody2D>();
            Transform transform1 = scene.Obj1.GetComponent<Transform>();

            Assert.AreEqual(Vector2.Zero, rb1.Velocity);

            rb1.ApplyLinearImpulse(new Vector2(50, 50));

            Assert.AreNotEqual(Vector2.Zero, rb1.Velocity);
            Assert.AreEqual(Vector2.Zero, transform1.Position);

            scene.Step(200);
            Assert.AreNotEqual(Vector2.Zero, transform1.Position);

            rb1.Velocity = Vector2.Zero;
            Assert.AreEqual(Vector2.Zero, rb1.Velocity);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestPosition()
        {
            SceneBody scene = new SceneBody();
            scene.OnStart();

            Transform transform1 = scene.Obj3.GetComponent<Transform>();

            transform1.Position = new Vector2(51251, -125123);
            Assert.AreEqual(new Vector2(51251, -125123), transform1.Position);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestRotation()
        {
            SceneBody scene = new SceneBody();
            scene.OnStart();

            Transform transform1 = scene.Obj3.GetComponent<Transform>();

            transform1.Rotation = 547246f;
            Assert.AreEqual(547246f, transform1.Rotation);

            scene.OnDestroy();
        }
    }
}
