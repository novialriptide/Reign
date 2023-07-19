using System;
using System.Collections.Generic;
using System.Numerics;
using Faraway.Engine.Components;

namespace Faraway.Main.Components
{
    public class PlayerInteractionHandler : Component
    {
        public bool IsActive => !GameObject.Scene.IsPaused;
        public List<SpaceCraft> GetSpaceCraftWithinRegion(Vector2 start, Vector2 end)
        {
            // ensure (x1,y1) and (x2,y2) will be topleft,bottomright
            float x1 = Math.Min(start.X, end.X);
            float y1 = Math.Min(start.Y, end.Y);
            float x2 = Math.Max(start.X, end.X);
            float y2 = Math.Max(start.Y, end.Y);

            // TODO: Finish implementation

            return new List<SpaceCraft>();
        }
    }
}
