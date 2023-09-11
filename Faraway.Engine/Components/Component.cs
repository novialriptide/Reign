namespace Faraway.Engine.Components
{
    public abstract class Component
    {
        public bool IsEnabled = true;
        public GameObject GameObject;
        public GameInstance GameInstance => GameObject.Scene.GameInstance;
        public Scene Scene => GameObject.Scene;

        /// <summary>
        /// Called when added to a gameobject.
        /// </summary>
        public virtual void OnAdd() { }
        /// <summary>
        /// Called once all OnAdd() calls are complete. Useful for calling
        /// components that are added after this component.
        ///
        /// </summary>
        public virtual void Start() { }
        public virtual void Update(double deltaTime) { }
        /// <summary>
        /// Only works if <see cref="BoxCollider2D"/> is attached
        /// </summary>
        public virtual void OnCollisionEnter(BoxCollider2D other) { }
        /// <summary>
        /// Only works if <see cref="BoxCollider2D"/> is attached
        /// </summary>
        public virtual void OnCollisionExit(BoxCollider2D other) { }
    }
}
