using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.WorldInterfaces;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Data
{
    public class WorldCatalogue : Catalogue<IWorldFactory>
    {
        protected override void FillFactories(Dictionary<string, IWorldFactory> factories)
        {
            factories.Add("Empty", new EmptyWorldFactory(physicsFactory.Origin));
        }
        private IPhysicsFactory physicsFactory;
        public WorldCatalogue(IPhysicsFactory physicsFactory)
        {
            this.physicsFactory = physicsFactory;
        }
    }
}
