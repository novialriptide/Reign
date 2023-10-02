using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Faraway.Engine.Tests")]
namespace Faraway.Engine.Components
{
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
        ///
        /// </summary>
        public virtual void Start() { }
        public virtual void Update(double deltaTime) { }
    }
}
