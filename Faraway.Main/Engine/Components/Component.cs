using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Engine.Components
{
    public abstract class Component
    {
        public GameObject GameObject;

        public virtual void Update(float deltaTime)
        {

        }
    }
}
