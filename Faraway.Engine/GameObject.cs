using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Faraway.Engine.Components;

namespace Faraway.Engine
{
    /// <summary>
    /// All <c>AddComponent()</c> calls should be used in the constructor.
    ///
    /// <code>
    /// public class PlayerInputHandler : GameObject
    /// {
    ///     public DragSelect DragSelect;
    ///     public PlayerInputHandler()
    ///     {
    ///         AddComponent(DragSelect = new DragSelect());
    ///     }
    /// }
    /// </code>
    /// 
    /// </summary>
    public class GameObject
    {
        public int ID { get; set; }
        /// <summary>
        /// The scene that the game object is in. Please do not re-assign this.
        /// </summary>
        public Scene Scene { get; set; }
        private Dictionary<int, Component> components = new Dictionary<int, Component>();
        public Component[] Components => components.Values.ToArray();

        /// <summary>
        /// Adds a component to the GameObject.
        /// </summary>
        /// <param name="component"></param>
        /// <returns>Returns itself so you can perform something like <c>Transform = AddComponent<Transform>(new Transform());</c></returns>
        public void AddComponent(Component component)
        {
            int code = component.GetType().GetHashCode();
            components[code] = component;
            component.GameObject = this;
            component.OnAdd();
        }
        public bool ContainsComponent<T>() where T : Component
        {
            int code = typeof(T).GetHashCode();
            return components.ContainsKey(code);
        }
        /// <summary>
        /// If you are calling any objects that require the GameInstance
        /// to be active, you must use this function.
        ///
        /// Set your <c>Texture</c> from <c>SpriteRenderer</c> here.
        /// 
        /// </summary>
        public virtual void OnAdd() { }
        internal void Update(double deltaTime)
        {
            foreach (var component in components.Values)
            {
                if (!component.IsEnabled)
                    continue;

                component.Update(deltaTime);
            }
        }
        public T GetComponent<T>() where T : Component
        {
            if (!ContainsComponent<T>())
                return null;

            int code = typeof(T).GetHashCode();
            return (T)components[code];
        }
    }
}
