using System.Numerics;
using Faraway.Engine.Components;

namespace Faraway.Main.Components
{
    public class PathFindingAgent : Component
    {
        public Transform Transform;
        public Vector2 Destination;

        private class Node
        {
            /// <summary>
            /// Heuristic: Estimated distance from the current node to the end node.
            /// </summary>
            public double H;
            /// <summary>
            /// Distance between the current and start node.
            /// </summary>
            public double G;
            /// <summary>
            /// Total cost of the node.
            /// </summary>
            public double F => G + H;
        }

        private Node[][] nodeMap;

        public override void Start()
        {
            Transform = GameObject.GetComponent<Transform>();

            base.Start();
        }

        public override void Update(double deltaTime)
        {
            Destination = Transform.Position;

            base.Update(deltaTime);
        }
    }
}
