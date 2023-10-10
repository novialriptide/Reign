using System.Numerics;
using Faraway.Engine.Components;

namespace Faraway.Engine.Tests.TestComponents
{
    [TestClass]
    public class TestTransform
    {
        private class ObjectTransform : GameObject
        {
            public ObjectTransform()
            {
                AddComponent(new Transform());
                base.OnAdd();
            }
        }
        private class SceneTransform : Scene
        {
            public ObjectTransform Obj1 = new ObjectTransform();
            public ObjectTransform Obj2 = new ObjectTransform();
            public ObjectTransform Obj3 = new ObjectTransform();
            public ObjectTransform Obj4 = new ObjectTransform();
            public ObjectTransform Obj5 = new ObjectTransform();
            public ObjectTransform Obj6 = new ObjectTransform();
            public ObjectTransform Obj7 = new ObjectTransform();
            public override void OnStart()
            {
                AddGameObject(Obj1);
                AddGameObject(Obj2);
                AddGameObject(Obj3);
                AddGameObject(Obj4);
                AddGameObject(Obj5);
                AddGameObject(Obj6);
                AddGameObject(Obj7);
                base.OnStart();
            }
        }

        [TestMethod]
        public void TestEnabled()
        {
            SceneTransform scene = new SceneTransform();
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
            SceneTransform scene = new SceneTransform();
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
            SceneTransform scene = new SceneTransform();
            scene.OnStart();

            Transform transform1 = scene.Obj1.GetComponent<Transform>();

            transform1.Rotation = 547246f;
            Assert.AreEqual(transform1.Rotation, 547246f);

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestNoParentChildRelation()
        {
            SceneTransform scene = new SceneTransform();
            scene.OnStart();

            Transform transform1 = scene.Obj1.GetComponent<Transform>();
            Assert.AreEqual(transform1.Children.Length, 0);
            Assert.AreEqual(transform1.Parent, null);
        }
        [TestMethod]
        public void TestSetParent()
        {
            SceneTransform scene = new SceneTransform();
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
            SceneTransform scene = new SceneTransform();
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
        [TestMethod]
        public void TestGetAllChildren()
        {
            SceneTransform scene = new SceneTransform();
            scene.OnStart();

            // Messy implementation that could've used an array... but it works.
            Transform transform1 = scene.Obj1.GetComponent<Transform>();
            Transform transform2 = scene.Obj2.GetComponent<Transform>();
            Transform transform3 = scene.Obj3.GetComponent<Transform>();
            Transform transform4 = scene.Obj4.GetComponent<Transform>();
            Transform transform5 = scene.Obj5.GetComponent<Transform>();
            Transform transform6 = scene.Obj6.GetComponent<Transform>();
            Transform transform7 = scene.Obj7.GetComponent<Transform>();

            transform1.AddChild(transform2);
            transform1.AddChild(transform3);
            transform3.AddChild(transform5);
            transform5.AddChild(transform4);
            transform6.AddChild(transform7);

            Transform[] allChildren = transform1.AllChildren;

            Assert.IsTrue(allChildren.Contains(transform2));
            Assert.IsTrue(allChildren.Contains(transform3));
            Assert.IsTrue(allChildren.Contains(transform4));
            Assert.IsTrue(allChildren.Contains(transform5));
            Assert.IsFalse(allChildren.Contains(transform6));
            Assert.IsFalse(allChildren.Contains(transform7));
            Assert.IsFalse(allChildren.Contains(null));

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestComponentsFromChildren()
        {
            SceneTransform scene = new SceneTransform();
            scene.OnStart();

            // Messy implementation that could've used an array... but it works.
            Transform transform1 = scene.Obj1.GetComponent<Transform>();
            Transform transform2 = scene.Obj2.GetComponent<Transform>();
            transform2.Parent = transform1;
            Transform transform3 = scene.Obj3.GetComponent<Transform>();
            transform3.Parent = transform1;
            Transform transform4 = scene.Obj4.GetComponent<Transform>();
            transform4.Parent = transform1;
            Transform transform5 = scene.Obj5.GetComponent<Transform>();
            transform5.Parent = transform1;
            Transform transform6 = scene.Obj6.GetComponent<Transform>();
            transform6.Parent = transform2;
            Transform transform7 = scene.Obj7.GetComponent<Transform>();

            Transform[] transforms = transform1.GetComponentFromChildren<Transform>();
            Assert.IsTrue(transforms.Contains(transform2));
            Assert.IsTrue(transforms.Contains(transform3));
            Assert.IsTrue(transforms.Contains(transform4));
            Assert.IsTrue(transforms.Contains(transform5));
            Assert.IsFalse(transforms.Contains(transform6));
            Assert.IsFalse(transforms.Contains(transform7));
            Assert.IsFalse(transforms.Contains(null));

            scene.OnDestroy();
        }
        [TestMethod]
        public void TestComponentsFromAllChildren()
        {
            SceneTransform scene = new SceneTransform();
            scene.OnStart();

            // Messy implementation that could've used an array... but it works.
            Transform transform1 = scene.Obj1.GetComponent<Transform>();
            Transform transform2 = scene.Obj2.GetComponent<Transform>();
            transform2.Parent = transform1;
            Transform transform3 = scene.Obj3.GetComponent<Transform>();
            transform3.Parent = transform1;
            Transform transform4 = scene.Obj4.GetComponent<Transform>();
            transform4.Parent = transform1;
            Transform transform5 = scene.Obj5.GetComponent<Transform>();
            transform5.Parent = transform1;
            Transform transform6 = scene.Obj6.GetComponent<Transform>();
            transform6.Parent = transform2;
            Transform transform7 = scene.Obj7.GetComponent<Transform>();

            Transform[] transforms = transform1.GetComponentFromAllChildren<Transform>();
            Assert.IsTrue(transforms.Contains(transform2));
            Assert.IsTrue(transforms.Contains(transform3));
            Assert.IsTrue(transforms.Contains(transform4));
            Assert.IsTrue(transforms.Contains(transform5));
            Assert.IsTrue(transforms.Contains(transform6));
            Assert.IsFalse(transforms.Contains(transform7));
            Assert.IsFalse(transforms.Contains(null));

            scene.OnDestroy();
        }
    }
}
