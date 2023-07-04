using System.Collections.Generic;
using System.ComponentModel.Design;
using Faraway.Main.Engine.Components;

namespace Faraway.Main.Engine
{
    public class GameObject
    {
        public int ID { get; set; }
        /// <summary>
        /// The scene that the game object is in. Please do not re-assign this.
        /// </summary>
        public Scene Scene { get; set; }
        private List<Component> components = new List<Component>();

        /// <summary>
        /// Adds a component to the GameObject.
        /// </summary>
        /// <param name="component"></param>
        /// <returns>Returns itself so you can perform something like <c>Transform = AddComponent<Transform>(new Transform());</c></returns>
        public void AddComponent(Component component)
        {
            components.Add(component);
            component.GameObject = this;
        }
        public bool ContainsComponent<T>() where T : Component
        {
            foreach (var component in components)
                if (component.GetType() == typeof(T))
                    return true;

            return false;
        }
        /// <summary>
        /// If you are calling any objects that require the GameInstance
        /// to be active, you must use this function.
        /// </summary>
        public virtual void OnAdd() { }
        public virtual void Update(double deltaTime) { }
        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
                if (component.GetType().Equals(typeof(T)))
                    return (T)component;

            return null;
        }
    }
}
