using System;
using System.Collections.Generic;
using System.Numerics;
using Faraway.Engine.Components;
using Microsoft.Xna.Framework.Input;

namespace Faraway.Main.Components
{
    public class MouseInput
    {
        private Vector2 start;
        private Vector2 end;
        public bool IsActive => !GameObject.Scene.IsPaused;
        public List<DragSelectElement> GetLeftDragRectangle(List<DragSelectElement> elements)
        {
            // ensure (x1,y1) and (x2,y2) will be topleft,bottomright
            float x1 = Math.Min(start.X, end.X);
            float y1 = Math.Min(start.Y, end.Y);
            float x2 = Math.Max(start.X, end.X);
            float y2 = Math.Max(start.Y, end.Y);

            // TODO: Finish implementation

            return new List<DragSelectElement>();
        }
        public override void Update(float deltaTime)
        {
            if ()

            base.Update(deltaTime);
        }
    }
}
