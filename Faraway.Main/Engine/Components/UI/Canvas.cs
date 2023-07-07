using System.Collections.Generic;

namespace Faraway.Main.Engine.Components.UI
{
    public class Canvas : Component
    {
        public List<GameObject> Containers
        {
            get
            {
                Transform[] transforms = GameObject.GetComponent<Transform>().GetChildren();
                List<GameObject> value = new List<GameObject>();
                foreach (Transform transform in transforms)
                    value.Add(transform.GameObject);

                return value;
            }
        }
        public Canvas() { }
    }
}
