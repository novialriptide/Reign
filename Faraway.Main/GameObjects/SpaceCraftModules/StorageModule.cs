using System;

namespace Faraway.Main.GameObjects.SpaceCraftModules
{
    internal class StorageModule : SpaceCraftModule
    {
        public override string Name => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public int StorageCapacity => 10_000;
    }
}
