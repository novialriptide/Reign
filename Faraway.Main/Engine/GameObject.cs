using Faraway.Main.Engine.Components;
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

        public void AddComponent(Component component)
        {
            components.Add(component);
            component.GameObject = this;
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
