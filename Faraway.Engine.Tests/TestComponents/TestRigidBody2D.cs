using System.Numerics;
using Faraway.Engine.Components;

namespace Faraway.Engine.Tests
{
    [Ignore]
    [TestClass]
    public class TestRigidBody2D
    {
        private class ObjectBody : GameObject
        {
            private BoxCollider2D collider;

            public ObjectBody()
            {
                AddComponent(new Transform());
                AddComponent(collider = new BoxCollider2D());
                AddComponent(new RigidBody2D());
                collider.Size.X = 150;
                collider.Size.Y = 150;
                base.OnAdd();
            }
        }
        private class ObjectCollider : GameObject
        {
            private BoxCollider2D collider;

            public ObjectCollider()
            {
                AddComponent(new Transform());
                AddComponent(collider = new BoxCollider2D());
                collider.Size.X = 150;
                collider.Size.Y = 150;
                base.OnAdd();
            }
        }
        private class SceneBody : Scene
        {
            public ObjectBody Obj1 = new ObjectBody();
            public ObjectCollider Obj2 = new ObjectCollider();
            public override void OnStart()
            {
                AddGameObject(Obj1);
                AddGameObject(Obj2);
                base.OnStart();
            }
        }

        [TestMethod]
        public void TestInitSuccessNoChildren()
        {
            SceneBody scene = new SceneBody();
            RigidBody2D rb1 = scene.Obj1.GetComponent<RigidBody2D>();

            Assert.IsNull(rb1.Body);
            scene.OnStart();
            rb1.Update(0f);

            Assert.IsTrue(rb1.IsEnabled);
            Assert.AreEqual(BodyType.Dynamic, rb1.BodyType);
            Assert.AreEqual(1, rb1.BoxCollider2Ds.Count);
            
            // Internal attribute checks
            Assert.IsNotNull(rb1.Body);
            Assert.IsNotNull(rb1.Simulation);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestInitSuccessWithChildren()
        {
            SceneBody scene = new SceneBody();
            scene.Obj2.GetComponent<Transform>().Parent = scene.Obj1.GetComponent<Transform>();
            RigidBody2D rb1 = scene.Obj1.GetComponent<RigidBody2D>();

            Assert.IsNull(rb1.Body);
            scene.OnStart();
            rb1.Update(0f);

            Assert.IsTrue(rb1.IsEnabled);
            Assert.AreEqual(BodyType.Dynamic, rb1.BodyType);
            Assert.AreEqual(1, rb1.BoxCollider2Ds.Count);
            
            // Internal attribute checks
            Assert.IsNotNull(rb1.Body);
            Assert.IsNotNull(rb1.Simulation);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestApplyRotationVelocity()
        {
            SceneBody scene = new SceneBody();

            scene.OnStart();

            RigidBody2D rb1 = scene.Obj1.GetComponent<RigidBody2D>();

            Assert.AreEqual(0, rb1.AngularVelocity);
            rb1.ApplyAngularImpulse(200);
            Assert.AreEqual(0, rb1.AngularVelocity);

            scene.Step(200);
            Assert.IsTrue(rb1.AngularVelocity != 0);

            rb1.AngularVelocity = 0;
            Assert.AreEqual(0, rb1.AngularVelocity);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestApplyVelocity()
        {
            SceneBody scene = new SceneBody();

            scene.OnStart();

            RigidBody2D rb1 = scene.Obj1.GetComponent<RigidBody2D>();

            Assert.AreEqual(Vector2.Zero, rb1.Velocity);
            rb1.ApplyLinearImpulse(new Vector2(50, 50));
            Assert.AreEqual(Vector2.Zero, rb1.Velocity);

            scene.Step(200);
            Assert.IsTrue(rb1.Velocity != Vector2.Zero);

            rb1.Velocity = Vector2.Zero;
            Assert.AreEqual(Vector2.Zero, rb1.Velocity);

            scene.OnDestroy();
        }
    }
}
