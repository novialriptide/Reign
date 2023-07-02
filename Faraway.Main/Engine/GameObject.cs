﻿using Faraway.Main.Engine.Components;
using System.Collections.Generic;
using System.ComponentModel.Design;

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
        public GameObject Parent;

        /// <summary>
        /// Adds a component to the GameObject.
        /// </summary>
        /// <param name="component"></param>
        /// <returns>Returns itself so you can perform something like <c>Transform = AddComponent<Transform>(new Transform());</c></returns>
        public T AddComponent<T>(Component component) where T : Component
        {
            components.Add(component);
            component.GameObject = this;

            return (T)component;
        }
        public bool ContainsComponent<T>() where T : Component
        {
            foreach (var component in components)
                if (component.GetType() == typeof(T))
                    return true;

            return false;
        }
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
