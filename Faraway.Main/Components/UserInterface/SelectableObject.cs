using System.Collections.Generic;
using Faraway.Engine.Components;

namespace Faraway.Main.Components.UserInterface
{
    public class SelectableObject : Component
    {
        public List<BoxCollider2D> BoxCollider2Ds = new List<BoxCollider2D>();
    }
}
