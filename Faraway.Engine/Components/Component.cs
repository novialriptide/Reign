namespace Faraway.Engine.Components
{
    public abstract class Component
    {
        public GameObject GameObject;

        /// <summary>
        /// Called when added to a gameobject.
        /// </summary>
        public virtual void OnAdd() { }
        /// <summary>
        /// Called once all OnAdd() calls are complete. Useful for calling
        /// components that are added after this component.
        ///
        /// TODO: Implement this.
        /// </summary>
        public virtual void Start() { }
        public virtual void Update(double deltaTime)
        {
            
        }
    }
}
