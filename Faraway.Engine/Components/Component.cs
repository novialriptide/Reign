using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Engine.Components
{
    public abstract class Component
    {
        public GameObject GameObject;

        /// <summary>
        /// Used to assign textures or anything that requires a
        /// <c>GameInstance</c> to be active.
        /// </summary>
        public virtual void OnAdd()
        {

        }

        public virtual void Update(double deltaTime)
        {

        }
    }
}
