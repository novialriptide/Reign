using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Faraway.Engine.Tests")]
namespace Faraway.Engine.Components
{
    public struct CollisionData
    {
        public readonly GameObject GameObject;
        public readonly RigidBody2D RigidBody2D => GameObject.GetComponent<RigidBody2D>();
        public readonly BoxCollider2D BoxCollider2D => GameObject.GetComponent<BoxCollider2D>();

        public CollisionData(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }

    public abstract class Component
    {
        public bool IsEnabled = true;
        public GameObject GameObject;

        /// <summary>
        /// Called when added to a gameobject.
        /// </summary>
        public virtual void OnAdd() { }
        /// <summary>
        /// Called once all OnAdd() calls are complete. Useful for calling
        /// components that are added after this component.
        /// </summary>
        public virtual void Start() { }
        public virtual void OnDestroy() { }
        public virtual void Update(double deltaTime) { }
        public virtual void OnCollisionEnter(CollisionData collisionData) { }
        public virtual void OnCollisionWhile(CollisionData collisionData) { }
        public virtual void OnCollisionExit(CollisionData collisionData) { }
        public virtual void OnTriggerEnter(CollisionData collisionData) { }
        public virtual void OnTriggerWhile(CollisionData collisionData) { }
        public virtual void OnTriggerExit(CollisionData collisionData) { }
    }
}
