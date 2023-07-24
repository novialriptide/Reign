using System;
using Microsoft.Xna.Framework.Graphics;

namespace Faraway.Main.GameObjects.SpaceCraftModules
{
    public class IonTurret : TurretModule
    {
        public override string Name => "Ion Turret";

        public override string Description => throw new NotImplementedException();
        public override void OnAdd()
        {
            Sprite2D.Texture = Scene.GameInstance.Content.Load<Texture2D>("baseModule");
            base.OnAdd();
        }
    }
}
