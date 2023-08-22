﻿using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Faraway.Engine;
using Faraway.Engine.Components;
using Faraway.Engine.MathExtended;
using Faraway.Main.Components;
using Faraway.Main.Components.UserInterface;
using Faraway.Main.GameObjects.SpaceCraftModules;
using Faraway.Main.Models;

namespace Faraway.Main.GameObjects
{
    public class SpaceCraft : GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Player Owner;
        public static int ModulePixelWidth = 32;
        public static int ModulePixelHeight = 32;
        private Dictionary<(int x, int y), SpaceCraftModule> modules = new Dictionary<(int x, int y), SpaceCraftModule>();
        public List<SpaceCraftModule> Modules => modules.Values.ToList();
        public Vector2? Destination = null;
        public float Speed
        {
            get
            {
                float value = 0;
                List<SpaceCraftModule> mods = GetModules<ThrusterModule>();
                foreach (ThrusterModule mod in mods)
                    value += mod.Speed;

                return value;
            }
        }

        /* Components */
        public Transform Transform;
        public RigidBody2D RigidBody2D;
        public PathFindingAgent PathFindingAgent;
        public List<BoxCollider2D> BoxCollider2Ds = new List<BoxCollider2D>(); // Referenced to the registered `SpaceCraftModule`s.
        public SelectableObject SelectableObject;

        public SpaceCraft(Player owner)
        {
            Owner = owner;
            AddComponent(Transform = new Transform());
            AddComponent(RigidBody2D = new RigidBody2D());
            AddComponent(PathFindingAgent = new PathFindingAgent());
            AddComponent(SelectableObject = new SelectableObject());
            SelectableObject.BoxCollider2Ds = BoxCollider2Ds;
        }
        public override void OnAdd()
        {
            var commandCenter = new CommandCenterModule();
            SetModule(0, 0, commandCenter);

            base.OnAdd();
        }

        /// <summary>
        /// Returns <c>true</c> if placing a module at <c>x</c> and <c>y</c> is valid.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsValidInsertionPosition(int x, int y)
        {
            if (modules.ContainsKey((x, y)))
                return false;

            // TODO: Add handler if the module would be a potential neighbor.

            return true;
        }
        /// <summary>
        /// Returns a SpaceCraftModule object, returns null if it doesn't exist.
        /// </summary>
        public SpaceCraftModule GetModule(int x, int y)
        {
            if (!modules.ContainsKey((x, y)))
                return null;

            return modules[(x, y)];
        }
        /// <summary>
        /// Returns a list of modules.
        /// </summary>
        public List<SpaceCraftModule> GetModules<T>() where T : SpaceCraftModule
        {
            List<SpaceCraftModule> value = new List<SpaceCraftModule>();
            foreach (SpaceCraftModule module in Modules)
                if (module.GetType() == typeof(T))
                    value.Add(module);

            return value;
        }
        /// <summary>
        /// Sets a SpaceCraftModule at the specified 
        /// coordinates, does nothing if position is invalid.
        /// </summary>
        public void SetModule(int x, int y, SpaceCraftModule module)
        {
            if (!IsValidInsertionPosition(x, y))
                return;

            var t = module.GetComponent<Transform>();
            BoxCollider2Ds.Add(module.GetComponent<BoxCollider2D>());
            t.Position += new Vector2(x * ModulePixelWidth, y * ModulePixelHeight);
            t.Parent = Transform;
            Scene.AddGameObject(module);
            modules.Add((x, y), module);
        }
        /// <summary>
        /// Moves to the specified position. 
        /// </summary>
        public void SetWaypoint(Vector2 waypoint)
        {
            /*
             * PRIORITY TODO: Make the ship rotate towards the target position, then move foward.
             */

            Vector2 velocity = MathV.SetMagnitude(waypoint - RigidBody2D.WorldCenter, 15552);
            RigidBody2D.Velocity = velocity;
        }
    }
}
