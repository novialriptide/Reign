﻿using System.Numerics;
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
            public override void OnStart()
            {
                AddGameObject(Obj1);
                AddGameObject(Obj2);
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
    }
}
