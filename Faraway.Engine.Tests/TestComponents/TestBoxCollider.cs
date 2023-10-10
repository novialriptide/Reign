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
                AddComponent(collider = new BoxCollider2D());
                collider.Size.X = 150;
                collider.Size.Y = 150;
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
    }
}
