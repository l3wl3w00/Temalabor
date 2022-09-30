using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.WorldInterfaces;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Worlds
{
    public class EmptyWorldFactory:IWorldFactory
    {
        private IPositionUnit initialHeroPosition;

        public EmptyWorldFactory(IPositionUnit initialHeroPosition)
        {
            this.initialHeroPosition = initialHeroPosition;
        }

        public World Create()
        {
            GameObjectContainer gameObjectContainer = new GameObjectContainer();
            gameObjectContainer.Hero = new Hero(100, initialHeroPosition);
            return new World(gameObjectContainer);
        }
    }
}
