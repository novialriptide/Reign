using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faraway.Main.Models.SpaceCraftModules
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
