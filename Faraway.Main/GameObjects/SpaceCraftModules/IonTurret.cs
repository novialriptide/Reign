using System;

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
