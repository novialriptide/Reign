using System.Collections.Generic;
using System.ComponentModel.Design;
using Faraway.Engine.Components;

namespace Faraway.Engine
{
    public class GameObject
    {
        public int ID { get; set; }
        /// <summary>
        /// The scene that the game object is in. Please do not re-assign this.
        /// </summary>
        public Scene Scene { get; set; }
        private Dictionary<int, Component> components = new Dictionary<int, Component>();

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
        }
        public bool ContainsComponent<T>() where T : Component
        {
            int code = typeof(T).GetHashCode();
            return components.ContainsKey(code);
        }
        /// <summary>
        /// If you are calling any objects that require the GameInstance
        /// to be active, you must use this function.
        /// </summary>
        public virtual void OnAdd() { }
        public virtual void Update(double deltaTime) { }
        public T GetComponent<T>() where T : Component
        {
            if (!ContainsComponent<T>())
                return null;

            int code = typeof(T).GetHashCode();
            return (T)components[code];
        }
    }
}
