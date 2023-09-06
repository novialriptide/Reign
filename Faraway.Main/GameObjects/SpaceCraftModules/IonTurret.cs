using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Main.GameObjects.SpaceCraftModules
{
    public class IonTurret : TurretModule
    {
        public override string Name => "Ion Turret";

        public override string Description => throw new NotImplementedException();
        public override void OnAdd()
        {
            Sprite2D.LoadTexureFromFile("baseModule.png");
            base.OnAdd();
        }
    }
}
