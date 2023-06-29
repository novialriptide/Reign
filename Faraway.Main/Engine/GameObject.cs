﻿using Faraway.Main.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Engine
{
    public class GameObject
    {
        public int ID { get; set; }
        private List<Component> components = new List<Component>();

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
        public virtual void Update(float gameTime) { }
        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
                if (component.GetType().Equals(typeof(T)))
                    return (T)component;

            return null;
        }
    }
}
